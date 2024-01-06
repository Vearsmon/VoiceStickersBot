using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.ShowAllResults;

public class ShowAllSwitchKeyboardStickersResult : ShowAllCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public Guid StickerPackId { get; }
    
    public string BotMessageId { get; }

    public ShowAllSwitchKeyboardStickersResult(
        long chatId,
        InlineKeyboardDto keyboardDto,
        Guid stickerPackId,
        string botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
        StickerPackId = stickerPackId;
    }
}