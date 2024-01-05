using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeletePackHandlers;

public class DeletePackCancelHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeletePack;

    private readonly DeletePackCancelArguments commandArguments;

    public DeletePackCancelHandler(DeletePackCancelArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }
    
    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}