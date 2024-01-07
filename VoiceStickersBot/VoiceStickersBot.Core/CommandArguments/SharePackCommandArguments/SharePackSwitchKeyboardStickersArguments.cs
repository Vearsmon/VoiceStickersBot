namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackSwitchKeyboardStickersArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.SharePack;

    public SharePackStepName StepName => SharePackStepName.SwKbdSt;

    public readonly Guid StickerPackId;
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int StickersOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }

    public SharePackSwitchKeyboardStickersArguments(
        Guid stickerPackId, 
        int pageFrom, 
        PageChangeDirection direction, 
        int stickersOnPage,
        long chatId, 
        string botMessageId)
    {
        StickerPackId = stickerPackId;
        PageFrom = pageFrom;
        Direction = direction;
        StickersOnPage = stickersOnPage;
        ChatId = chatId;
        BotMessageId = botMessageId;
    }
}