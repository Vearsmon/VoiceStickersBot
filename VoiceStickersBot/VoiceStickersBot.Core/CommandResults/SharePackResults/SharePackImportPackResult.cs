namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackImportPackResult : SharePackCommandResultBase
{
    public override long ChatId { get; }
    public bool IsSucceeded { get; }

    public SharePackImportPackResult(long chatId, bool isSucceeded)
    {
        ChatId = chatId;
        IsSucceeded = isSucceeded;
    }
}