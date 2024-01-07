using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;

public class ShowAllCancelHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.ShowAll;

    private readonly ShowAllCancelArguments commandArguments;
    
    public ShowAllCancelHandler(ShowAllCancelArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }
    
    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}