namespace VoiceStickersBot.Core.CommandResults.StartResults;

public class StartStartResult : StartResultBase
{
    public override long ChatId { get; }
    
    public StartStartResult(long chatId)
    {
        ChatId = chatId;
    }
}