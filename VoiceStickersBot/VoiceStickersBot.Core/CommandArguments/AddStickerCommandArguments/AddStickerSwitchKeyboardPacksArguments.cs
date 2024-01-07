namespace VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

public class AddStickerSwitchKeyboardPacksArguments : IAddStickerCommandArguments
{
    public CommandType CommandType => CommandType.AddSticker;
    public AddStickerStepName StepName => AddStickerStepName.SwKbdPc;
    
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int PacksOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }
    
    public AddStickerSwitchKeyboardPacksArguments(
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