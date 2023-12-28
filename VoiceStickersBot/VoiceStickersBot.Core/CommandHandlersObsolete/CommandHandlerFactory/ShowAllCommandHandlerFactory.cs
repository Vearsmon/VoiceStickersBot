/*using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.Core.CommandHandlersObsolete.CommandHandlerFactory;

public class ShowAllCommandHandlerFactory : CommandHandlerFactoryBase<ShowAllCommandObsolete>
{
    public override Type CommandType => typeof(ShowAllCommandObsolete);
    
    protected override ICommandHandler CreateCommandHandler(ShowAllCommandObsolete commandObsolete)
    {
        return new ShowAllHandler(commandObsolete);
    }
}*/