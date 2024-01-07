namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSendFileInstructionsResult : AddStickerCommandResultBase
{
    public override long ChatId { get; }
    public Guid StickerPackId { get; }
    public string StickerName { get; }
    
    public AddStickerSendFileInstructionsResult(long chatId, Guid stickerPackId, string stickerName)
    {
        ChatId = chatId;
        StickerPackId = stickerPackId;
        StickerName = stickerName;
    }
}