using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.Core.CommandResults.CreatePackResults;

public class CreatePackCommandResultBase : ICommandResult
{
    public CommandType CommandType => CommandType.CreatePack;

    public virtual long ChatId { get; }
}