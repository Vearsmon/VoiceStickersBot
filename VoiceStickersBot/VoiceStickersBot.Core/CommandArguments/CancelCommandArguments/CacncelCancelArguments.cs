namespace VoiceStickersBot.Core.CommandArguments.CancelCommandArguments;

public class CacncelCancelArguments : ICancelCommandArguments
{
    public CommandType CommandType => CommandType.Cancel;
    public CancelStepName StepName => CancelStepName.Cancel;
    
    public string ChatType { get; }
    public long ChatId { get; }
    
    public CacncelCancelArguments(string chatType, long chatId)
    {
        ChatType = chatType;
        ChatId = chatId;
    }
}