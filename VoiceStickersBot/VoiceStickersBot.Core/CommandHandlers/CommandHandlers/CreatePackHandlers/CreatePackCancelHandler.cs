using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.CreatePackHandlers;

public class CreatePackCancelHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.CreatePack;
    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}