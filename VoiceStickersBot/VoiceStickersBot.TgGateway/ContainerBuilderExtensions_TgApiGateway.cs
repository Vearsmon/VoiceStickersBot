using Ninject;
using VoiceStickersBot.TgGateway.CommandResultHandlers;

namespace VoiceStickersBot.TgGateway;

public static class ContainerBuilderExtensions
{
    public static StandardKernel BindCommandResultHandlers(this StandardKernel containerBuilder)
    {
        containerBuilder.Bind<ICommandResultHandler>().To<ShowAllResultHandler>().InSingletonScope();
        containerBuilder.Bind<ICommandResultHandler>().To<CreatePackResultHandler>().InSingletonScope();
        containerBuilder.Bind<ICommandResultHandler>().To<AddStickerResultHandler>().InSingletonScope();
        return containerBuilder;
    }
}