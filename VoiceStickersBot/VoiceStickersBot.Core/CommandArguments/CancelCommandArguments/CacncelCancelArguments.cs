namespace VoiceStickersBot.Core.CommandArguments.CancelCommandArguments;

public class CacncelCancelArguments : ICancelCommandArguments
{
    public CommandType CommandType => CommandType.Cancel;
    public CancelStepName StepName => CancelStepName.Cancel;
    
    public long ChatId { get; }
    
    public CacncelCancelArguments(long chatId)
    {
        ChatId = chatId;
    }
}