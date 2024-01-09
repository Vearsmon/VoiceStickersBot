namespace VoiceStickersBot.Core.CommandResults.CancelResult;

public class CancelCancelResult : CancelResultBase
{
    public override long ChatId { get; }
    public string ChatType { get; }

    public CancelCancelResult(long chatId, string chatType)
    {
        ChatId = chatId;
        ChatType = chatType;
    }
}