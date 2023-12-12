using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class ShowAllHandler : ICommandHandler
{
    public Type CommandType => typeof(ShowAllCommand);

    private readonly ShowAllCommand command;
    
    public ShowAllHandler(ShowAllCommand command)
    {
        this.command = command;
    }
    
    public ICommandResult Handle()
    {
        return new ShowAllResult();
    }
}