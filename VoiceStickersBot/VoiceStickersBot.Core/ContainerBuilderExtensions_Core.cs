using Ninject;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

namespace VoiceStickersBot.TgGateway;

public static class ContainerBuilderExtensions
{
    public static StandardKernel BindCommandHandlerFactories(this StandardKernel containerBuilder)
    {
        containerBuilder.Bind<ICommandHandlerFactory>().To<ShowAllCommandHandlerFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandHandlerFactory>().To<CreatePackCommandHandlerFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandHandlerFactory>().To<AddStickerCommandHandlerFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandHandlerFactory>().To<DeletePackCommandHandlerFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandHandlerFactory>().To<DeleteStickerCommandHandlerFactory>().InSingletonScope();
        return containerBuilder;
    }
}