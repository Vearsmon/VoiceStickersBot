using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

public class DeletePackSwitchKeyboardStickersArguments : IDeletePackCommandArguments
{
    public CommandType CommandType => CommandType.DeletePack;
    public DeletePackStepName StepName => DeletePackStepName.SwKbdSt;
    
    public readonly Guid StickerPackId;
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int PacksOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }
    
    public DeletePackSwitchKeyboardStickersArguments(
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