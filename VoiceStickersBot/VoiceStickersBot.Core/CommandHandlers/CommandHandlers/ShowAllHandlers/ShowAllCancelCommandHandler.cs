using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;

public class ShowAllCancelCommandHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.ShowAll;

    private readonly ShowAllCancelCommandArguments commandArguments;
    
    public ShowAllCancelCommandHandler(ShowAllCancelCommandArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }
    
    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}