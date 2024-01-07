namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackSendImportInstructionsResult : SharePackCommandResultBase
{
    public override long ChatId { get; }

    public SharePackSendImportInstructionsResult(long chatId)
    {
        ChatId = chatId;
    }
}