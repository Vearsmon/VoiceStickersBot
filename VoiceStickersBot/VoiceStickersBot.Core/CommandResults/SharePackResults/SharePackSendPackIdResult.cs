namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackSendPackIdResult : SharePackCommandResultBase
{
    public override long ChatId { get; }
    public Guid StickerPackId { get; }
    
    public SharePackSendPackIdResult(long chatId, Guid stickerPackId)
    {
        ChatId = chatId;
        StickerPackId = stickerPackId;
    }
}