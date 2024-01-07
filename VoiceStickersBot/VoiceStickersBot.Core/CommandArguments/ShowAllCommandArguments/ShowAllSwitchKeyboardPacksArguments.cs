using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSwitchKeyboardPacksArguments : IShowAllCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public ShowAllStepName StepName => ShowAllStepName.SwKbdPc;
    
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int PacksOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }
    
    public ShowAllSwitchKeyboardPacksArguments(
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