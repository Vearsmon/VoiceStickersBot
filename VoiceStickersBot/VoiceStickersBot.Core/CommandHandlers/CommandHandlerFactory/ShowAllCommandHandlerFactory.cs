using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class ShowAllCommandHandlerFactory : CommandHandlerFactoryBase<ShowAllCommand>
{
    public override Type CommandType => typeof(ShowAllCommand);
    
    protected override ICommandHandler CreateCommandHandler(ShowAllCommand command)
    {
        return new ShowAllHandler(command);
    }
}