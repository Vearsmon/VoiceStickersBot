using Ninject;
using VoiceStickersBot.TgGateway.CommandArgumentsFactory;
using VoiceStickersBot.TgGateway.CommandResultHandlers;

namespace VoiceStickersBot.TgGateway;

public static class ContainerBuilderExtensions
{
    public static StandardKernel BindCommandArgumentsFactories(this StandardKernel containerBuilder)
    {
        containerBuilder.Bind<ICommandArgumentsFactory>().To<ShowAllCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<CreatePackCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<AddStickerCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<DeletePackCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<DeleteStickerCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<SharePackCommandArgumentsFactory>().InSingletonScope();
        
        containerBuilder.Bind<ICommandArgumentsFactory>().To<CancelCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<StartCommandArgumentsFactory>().InSingletonScope();
        
        return containerBuilder;
    }

    public static StandardKernel BindCommandResultHandlers(this StandardKernel containerBuilder)
    {
        containerBuilder.Bind<ICommandResultHandler>().To<ShowAllResultHandler>().InSingletonScope();
        containerBuilder.Bind<ICommandResultHandler>().To<CreatePackResultHandler>().InSingletonScope();
        containerBuilder.Bind<ICommandResultHandler>().To<AddStickerResultHandler>().InSingletonScope();
        containerBuilder.Bind<ICommandResultHandler>().To<DeletePackResultHandler>().InSingletonScope();
        containerBuilder.Bind<ICommandResultHandler>().To<DeleteStickerResultHandler>().InSingletonScope();
        containerBuilder.Bind<ICommandResultHandler>().To<SharePackResultHandler>().InSingletonScope();
        
        containerBuilder.Bind<ICommandResultHandler>().To<CancelResultHandler>().InSingletonScope();
        containerBuilder.Bind<ICommandResultHandler>().To<StartResultHandler>().InSingletonScope();
        
        return containerBuilder;
    }
}