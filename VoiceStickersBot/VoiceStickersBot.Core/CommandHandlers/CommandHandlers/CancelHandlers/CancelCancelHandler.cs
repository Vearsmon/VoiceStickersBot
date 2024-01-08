using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CancelCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.CancelResult;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.CancelHandlers;

public class CancelCancelHandler: ICommandHandler
{
    public CommandType CommandType => CommandType.Cancel;

    private readonly CacncelCancelArguments commandArguments;

    public CancelCancelHandler(CacncelCancelArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public async Task<ICommandResult> Handle()
    {
        return new CancelCancelResult(commandArguments.ChatId);
    }
}