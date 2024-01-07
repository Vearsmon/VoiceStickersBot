namespace VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

public class AddStickerSwitchKeyboardStickersArguments : IAddStickerCommandArguments
{
    public CommandType CommandType => CommandType.AddSticker;
    public AddStickerStepName StepName => AddStickerStepName.SwKbdSt;
    
    public readonly Guid StickerPackId;
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int StickersOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }
    
    public AddStickerSwitchKeyboardStickersArguments(
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