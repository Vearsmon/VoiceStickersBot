using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.CreatePackHandlers;

public class CreatePackCancelHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.CreatePack;
    
    private readonly CreatePackCancelArguments commandArguments;

    public CreatePackCancelHandler(CreatePackCancelArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}