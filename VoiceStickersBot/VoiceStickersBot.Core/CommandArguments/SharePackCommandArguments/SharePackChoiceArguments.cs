namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackChoiceArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.SharePack;

    public SharePackStepName StepName => SharePackStepName.Choice;
    public long ChatId { get; }
    public string BotMessageId { get; }
    
    public SharePackChoiceArguments(
        long chatId, 
        string botMessageId)
    {
        ChatId = chatId;
        BotMessageId = botMessageId;
    }
}