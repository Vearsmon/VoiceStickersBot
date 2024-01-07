namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackSwitchKeyboardPacksArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.SharePack;

    public SharePackStepName StepName => SharePackStepName.SwKbdPc;
    
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int PacksOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }
    
    public SharePackSwitchKeyboardPacksArguments(
        int pageFrom, 
        PageChangeDirection direction, 
        int packsOnPage,
        long chatId, 
        string botMessageId)
    {
        PageFrom = pageFrom;
        Direction = direction;
        PacksOnPage = packsOnPage;
        ChatId = chatId;
        BotMessageId = botMessageId;
    }
}