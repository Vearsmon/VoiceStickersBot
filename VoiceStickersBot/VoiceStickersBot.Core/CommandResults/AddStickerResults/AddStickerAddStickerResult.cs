namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerAddStickerResult : AddStickerCommandResultBase
{
    public override long ChatId { get; }
    public string StickerName { get; }
    public Guid StickerId { get; }

    public AddStickerAddStickerResult(long chatId, string stickerName, Guid stickerId)
    {
        ChatId = chatId;
        StickerName = stickerName;
        StickerId = stickerId;
    }
}