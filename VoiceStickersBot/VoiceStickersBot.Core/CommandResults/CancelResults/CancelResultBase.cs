using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.Core.CommandResults.CancelResult;

public class CancelResultBase : ICommandResult
{
    public CommandType CommandType => CommandType.Cancel;
    public virtual long ChatId { get; }
}