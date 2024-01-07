namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackChoiceArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.SharePack;

    public SharePackStepName StepName => SharePackStepName.Choice;
    public long ChatId { get; }
    public int? BotMessageId { get; }
    
    public SharePackChoiceArguments(
        long chatId, 
        int? botMessageId)
    {
        ChatId = chatId;
        BotMessageId = botMessageId;
    }
}