namespace VoiceStickersBot.Core.CommandArguments.StartCommandArguments;

public class StartStartArguments : IStartCommandArguments
{
    public CommandType CommandType => CommandType.Start;
    public StartStepName StepName => StartStepName.Start;
    
    public long ChatId { get; }
    
    public StartStartArguments(long chatId)
    {
        ChatId = chatId;
    }
}