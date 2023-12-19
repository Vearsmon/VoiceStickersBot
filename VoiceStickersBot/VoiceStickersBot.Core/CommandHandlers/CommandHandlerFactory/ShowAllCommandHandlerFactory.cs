using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class ShowAllCommandHandlerFactory<TCommandArguments> : CommandHandlerFactoryBase<ShowAllStepName, TCommandArguments>
    where TCommandArguments : class, ICommandArguments<ShowAllStepName>
{
    public override CommandType CommandType => CommandType.ShowAll;
    private Dictionary<ShowAllStepName, Func<ICommandArguments<ShowAllStepName>, ICommandHandler>> stepHandlers = new() 
    {
        { ShowAllStepName.Cancel,  },
        { ShowAllStepName.SwitchKeyboardPacks,  },
        { ShowAllStepName.SwitchKeyboardStickers,  },
        { ShowAllStepName.SendSticker,  }
    }
    
    protected override ICommandHandler CreateCommandHandler(TCommandArguments commandArguments)
    {
        return new ShowAllHandler(commandArguments.);
    }
}