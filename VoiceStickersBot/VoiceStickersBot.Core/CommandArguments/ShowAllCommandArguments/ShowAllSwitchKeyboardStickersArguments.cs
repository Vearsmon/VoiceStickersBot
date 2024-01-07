using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSwitchKeyboardStickersArguments : IShowAllCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public ShowAllStepName StepName => ShowAllStepName.SwKbdSt;

    public readonly Guid StickerPackId;
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int StickersOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }

    public ShowAllSwitchKeyboardStickersArguments(
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