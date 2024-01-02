using Ninject;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Infra.VSBApplication;
using VoiceStickersBot.Infra.VSBApplication.Settings;
using VoiceStickersBot.TgGateway;
using VoiceStickersBot.TgGateway.CommandResultHandlers;

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
        var botClient = new TelegramBotClient(ApplicationSettings.Settings["token"]);

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
    }

    protected override void ConfigureContainer(StandardKernel containerBuilder)
    {
        containerBuilder.Bind<ICommandArgumentsFactory>().To<ShowAllCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<CreatePackCommandArgumentsFactory>().InSingletonScope();
        
        
        containerBuilder.Bind<ICommandHandlerFactory>().To<ShowAllCommandHandlerFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandHandlerFactory>().To<CreatePackCommandHandlerFactory>().InSingletonScope();
        
        containerBuilder.Bind<ICommandResultHandler>().To<ShowAllResultHandler>().InSingletonScope();
        containerBuilder.Bind<ICommandResultHandler>().To<CreatePackResultHandler>().InSingletonScope();

        base.ConfigureContainer(containerBuilder);
    }
}