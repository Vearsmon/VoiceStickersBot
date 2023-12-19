using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class ShowAllCommandHandlerFactory : CommandHandlerFactoryBase<IShowAllCommandArguments>
{
    public override CommandType CommandType => CommandType.ShowAll;

    private readonly Dictionary<ShowAllStepName, Func<IShowAllCommandArguments, ICommandHandler>> stepHandlerBuilders =
        new()
        {
            { ShowAllStepName.Cancel, commandArguments =>  },
            { ShowAllStepName.SwitchKeyboardPacks, commandArguments },
            ShowAllStepName.SwitchKeyboardStickers,
            ShowAllStepName.SendSticker
        };

    protected override ICommandHandler CreateCommandHandler(IShowAllCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}