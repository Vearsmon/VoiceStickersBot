using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class SharePackSwitchKeyboardPacksCommandArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public SharePackStepName StepName => SharePackStepName.SwKbdPc;
    
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int PacksOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }
    
    public SharePackSwitchKeyboardPacksCommandArguments(
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