using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.Core.CommandResults.DeletePackResults;

public class DeletePackCommandResultBase : ICommandResult
{
    public CommandType CommandType => CommandType.DeletePack;

    public virtual long ChatId { get; }
}