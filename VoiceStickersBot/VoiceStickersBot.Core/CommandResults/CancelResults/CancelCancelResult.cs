namespace VoiceStickersBot.Core.CommandResults.CancelResult;

public class CancelCancelResult : CancelResultBase
{
    public override long ChatId { get; }

    public CancelCancelResult(long chatId)
    {
        ChatId = chatId;
    }
}