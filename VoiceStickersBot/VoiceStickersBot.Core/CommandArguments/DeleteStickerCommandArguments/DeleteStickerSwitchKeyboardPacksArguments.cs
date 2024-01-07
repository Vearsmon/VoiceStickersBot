using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;

public class DeleteStickerSwitchKeyboardPacksArguments : IDeleteStickerCommandArguments
{
    public CommandType CommandType => CommandType.DeleteSticker;
    public DeleteStickerStepName StepName => DeleteStickerStepName.SwKbdPc;
    
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int PacksOnPage { get; }
    public long ChatId { get; }
    public int? BotMessageId { get; }
    
    public DeleteStickerSwitchKeyboardPacksArguments(
        int pageFrom,
        PageChangeDirection direction,
        int packsOnPage,
        long chatId,
        int? botMessageId)
    {
        PageFrom = pageFrom;
        Direction = direction;
        PacksOnPage = packsOnPage;
        ChatId = chatId;
        BotMessageId = botMessageId;
    }
}