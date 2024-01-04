using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public abstract class AddStickerCommandResultBase : ICommandResult
{
    public CommandType CommandType => CommandType.AddSticker;

    public virtual long ChatId { get; }
}