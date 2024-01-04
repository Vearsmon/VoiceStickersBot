using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.Core.CommandResults;

public interface ICommandResult
{
    public CommandType CommandType { get; }

    public long ChatId { get; }
}