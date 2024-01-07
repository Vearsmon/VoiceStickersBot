using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.Core.CommandResults.DeleteStickerResults;

public class DeleteStickerCommandResultBase : ICommandResult
{
    public CommandType CommandType => CommandType.DeleteSticker;

    public virtual long ChatId { get; }
}