namespace VoiceStickersBot.Core.CommandArguments.StartCommandArguments;

public class StartStartArguments : IStartCommandArguments
{
    public CommandType CommandType => CommandType.Start;
    public StartStepName StepName => StartStepName.Start;
    
    public string ChatType { get; }
    public long ChatId { get; }
    
    public StartStartArguments(string chatType, long chatId)
    {
        ChatType = chatType;
        ChatId = chatId;
    }
}