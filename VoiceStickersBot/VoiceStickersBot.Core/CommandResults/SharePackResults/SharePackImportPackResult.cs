namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackImportPackResult : SharePackCommandResultBase
{
    public override long ChatId { get; }
    public bool IsSucceeded { get; }
    public string PackName { get; }
    public string ChatType { get; }

    public SharePackImportPackResult(long chatId, bool isSucceeded, string packName, string chatType)
    {
        ChatId = chatId;
        IsSucceeded = isSucceeded;
        PackName = packName;
        ChatType = chatType;
    }
}