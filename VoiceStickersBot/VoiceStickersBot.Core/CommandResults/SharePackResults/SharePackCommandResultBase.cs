using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public abstract class SharePackCommandResultBase : ICommandResult
{
    public CommandType CommandType => CommandType.SharePack;

    public virtual long ChatId { get; }
}