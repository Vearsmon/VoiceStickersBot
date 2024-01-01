using Ninject;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Infra.VSBApplication;
using VoiceStickersBot.TgGateway;
using VoiceStickersBot.TgGateway.CommandResultHandlers;

using CancellationTokenSource cts = new();
var host = new TgApiGatewayHost();

await host.RunAsync(() => cts.Token);

Console.ReadLine();
cts.Cancel();


public class TgApiGatewayHost : VsbApplicationBase
{
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
        var botClient = new TelegramBotClient("5989359414:AAF5EHNI513b6kNi1In6gjqUBi9HKgsGRrM");

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        var handler = container.Get<TgApiGateway>();
        botClient.StartReceiving(
            handler.HandleUpdateAsync,
            handler.HandlePollingErrorAsync,
            receiverOptions,
            cancellationToken
        );

        var me = await botClient.GetMeAsync(cancellationToken);
        Console.WriteLine($"Start listening for @{me.Username}");
    }

    protected override void ConfigureContainer(StandardKernel containerBuilder)
    {
        containerBuilder.Bind<ICommandArgumentsFactory>().To<ShowAllCommandArgumentsFactory>().InSingletonScope();

        containerBuilder.Bind<ICommandResultHandler>().To<ShowAllResultHandler>().InSingletonScope();

        // containerBuilder.Bind<ICommandHandlerFactory>().To<ShowAllCommandHandlerFactory>().InSingletonScope();

        base.ConfigureContainer(containerBuilder);
    }
}