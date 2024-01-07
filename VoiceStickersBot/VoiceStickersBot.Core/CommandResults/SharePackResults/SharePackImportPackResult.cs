namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackImportPackResult : SharePackCommandResultBase
{
    public override long ChatId { get; }
    
    public SharePackImportPackResult(long chatId)
    {
        ChatId = chatId;
    }
}