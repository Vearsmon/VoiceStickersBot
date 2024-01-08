using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.Core.CommandResults.StartResults;

public class StartResultBase : ICommandResult
{
    public CommandType CommandType => CommandType.Start;
    public virtual long ChatId { get; }
}