using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSwitchKeyboardStickersCommandArguments : IShowAllCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public ShowAllStepName StepName => ShowAllStepName.SwitchKeyboardStickers;

    public readonly Guid StickerPackId;
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int PacksOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }

    public ShowAllSwitchKeyboardStickersCommandArguments(
        Guid stickerPackId, 
        int pageFrom, 
        PageChangeDirection direction, 
        int packsOnPage,
        long chatId, 
        string botMessageId)
    {
        StickerPackId = stickerPackId;
        PageFrom = pageFrom;
        Direction = direction;
        PacksOnPage = packsOnPage;
        ChatId = chatId;
        BotMessageId = botMessageId;
    }
}