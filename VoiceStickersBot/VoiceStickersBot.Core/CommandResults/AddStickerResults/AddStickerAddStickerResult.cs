namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerAddStickerResult : AddStickerCommandResultBase
{
    public override long ChatId { get; }
    public string StickerName { get; }
    public string FileId { get; }

    public AddStickerAddStickerResult(long chatId, string stickerName, string fileId)
    {
        ChatId = chatId;
        StickerName = stickerName;
        FileId = fileId;
    }
}