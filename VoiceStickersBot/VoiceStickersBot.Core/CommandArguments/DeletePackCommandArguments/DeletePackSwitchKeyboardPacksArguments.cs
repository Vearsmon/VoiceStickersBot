namespace VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

public class DeletePackSwitchKeyboardPacksArguments : IDeletePackCommandArguments
{
    public CommandType CommandType => CommandType.DeletePack;
    public DeletePackStepName StepName => DeletePackStepName.SwKbdPc;
    
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int PacksOnPage { get; }
    public long ChatId { get; }
    public string BotMessageId { get; }
    
    public DeletePackSwitchKeyboardPacksArguments(
        int pageFrom,
        PageChangeDirection direction,
        int packsOnPage,
        long chatId,
        string botMessageId)
    {
        ChatId = chatId;
        BotMessageId = botMessageId;
        PageFrom = pageFrom;
        Direction = direction;
        PacksOnPage = packsOnPage;
    }
}