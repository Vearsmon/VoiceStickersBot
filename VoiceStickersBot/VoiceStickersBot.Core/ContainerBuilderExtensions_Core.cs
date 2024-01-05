using Ninject;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

namespace VoiceStickersBot.TgGateway;

public static class ContainerBuilderExtensions
{
    public static StandardKernel BindCommandArgumentsFactories(this StandardKernel containerBuilder)
    {
        containerBuilder.Bind<ICommandArgumentsFactory>().To<ShowAllCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<CreatePackCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<AddStickerCommandArgumentsFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandArgumentsFactory>().To<DeletePackCommandArgumentsFactory>().InSingletonScope();
        return containerBuilder;
    }

    public static StandardKernel BindCommandHandlerFactories(this StandardKernel containerBuilder)
    {
        containerBuilder.Bind<ICommandHandlerFactory>().To<ShowAllCommandHandlerFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandHandlerFactory>().To<CreatePackCommandHandlerFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandHandlerFactory>().To<AddStickerCommandHandlerFactory>().InSingletonScope();
        containerBuilder.Bind<ICommandHandlerFactory>().To<DeletePackCommandHandlerFactory>().InSingletonScope();
        return containerBuilder;
    }
}