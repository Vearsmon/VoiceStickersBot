using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.Core.CommandResults.ShowAllResults;

public class ShowAllCommandResultBase : ICommandResult
{
    public CommandType CommandType => CommandType.ShowAll;

    public virtual long ChatId { get; }
}