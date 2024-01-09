using Ninject;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Infra.VSBApplication;
using VoiceStickersBot.Infra.VSBApplication.Settings;
using VoiceStickersBot.TgGateway;

using CancellationTokenSource cts = new();
var host = new TgApiGatewayHost();

await host.RunAsync(() => cts.Token);

Console.ReadLine();
cts.Cancel();

[Settings("TgApiGatewaySettings")]
public class TgApiGatewayHost : VsbApplicationBase
{
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
        var botClient = Container.Get<ITelegramBotClient>();

        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        var handler = Container.Get<TgApiGatewayService>();
        botClient.StartReceiving(
            handler.HandleUpdateAsync,
            handler.HandlePollingErrorAsync,
            receiverOptions,
            cancellationToken
        );

        var me = await botClient.GetMeAsync(cancellationToken);
        Console.WriteLine($"Start listening for @{me.Username}");

        var requestTimesPerGroup = new Dictionary<long, List<DateTime>>();
        var requestTimes = new List<DateTime>();
        while (true)
        {
            if (!handler.Requests.TryDequeue(out var request))
            {
                continue;
            }

            var now = DateTime.Now;
            var after = now - TimeSpan.FromSeconds(1);
            var requestsPerSecond = ((IEnumerable<DateTime>)requestTimes).Reverse().Count(t => t > after);
            if (requestsPerSecond > 25)
            {
                continue;
            }

            var chatId = handler.ExtractChatId(request);
            if (chatId is null)
                continue;

            if (requestTimesPerGroup.TryGetValue(chatId.Value, out var times))
            {
                after = now - TimeSpan.FromMinutes(1);
                requestsPerSecond = ((IEnumerable<DateTime>)times).Reverse().Count(t => t > after);
                if (requestsPerSecond > 15) continue;
            }
            else
            {
                requestTimesPerGroup[chatId.Value] = new List<DateTime> { now };
            }

            requestTimesPerGroup[chatId.Value].Add(now);

            await handler.Handle(botClient, request, cancellationToken);
        }
    }

    protected override void ConfigureContainer(StandardKernel containerBuilder)
    {
        containerBuilder
            .BindCommandArgumentsFactories()
            .BindCommandHandlerFactories()
            .BindCommandResultHandlers();

        var botClient = new TelegramBotClient(ApplicationSettings.Settings["token"]);

        containerBuilder.Bind<ITelegramBotClient>().ToConstant(botClient);

        base.ConfigureContainer(containerBuilder);
    }
}