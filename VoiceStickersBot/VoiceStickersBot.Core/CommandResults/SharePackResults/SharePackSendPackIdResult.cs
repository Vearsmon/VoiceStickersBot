namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackSendPackIdResult : SharePackCommandResultBase
{
    public override long ChatId { get; }
    public Guid StickerPackId { get; }
    public string ChatType { get; }
    
    public SharePackSendPackIdResult(long chatId, Guid stickerPackId, string chatType)
    {
        ChatId = chatId;
        StickerPackId = stickerPackId;
        ChatType = chatType;
    }
}