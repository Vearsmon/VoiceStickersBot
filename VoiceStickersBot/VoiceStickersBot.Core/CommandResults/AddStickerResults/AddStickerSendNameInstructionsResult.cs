namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSendNameInstructionsResult : AddStickerCommandResultBase
{
    public override long ChatId { get; }
    public Guid StickerPackId { get; }

    public AddStickerSendNameInstructionsResult(long chatId, Guid stickerPackId)
    {
        ChatId = chatId;
        StickerPackId = stickerPackId;
    }
}