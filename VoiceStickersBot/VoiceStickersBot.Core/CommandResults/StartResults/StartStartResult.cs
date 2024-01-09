namespace VoiceStickersBot.Core.CommandResults.StartResults;

public class StartStartResult : StartResultBase
{
    public override long ChatId { get; }
    public string ChatType { get; }
    
    public StartStartResult(long chatId, string chatType)
    {
        ChatId = chatId;
        ChatType = chatType;
    }
}