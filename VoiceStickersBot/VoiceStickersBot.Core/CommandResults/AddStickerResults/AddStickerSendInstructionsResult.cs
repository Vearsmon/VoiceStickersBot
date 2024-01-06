namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSendInstructionsResult : AddStickerCommandResultBase
{
    public override long ChatId { get; }
    public Guid StickerPackId { get; }

    public AddStickerSendInstructionsResult(long chatId, Guid stickerPackId)
    {
        ChatId = chatId;
        StickerPackId = stickerPackId;
    }
}