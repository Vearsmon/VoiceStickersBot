using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.DeleteStickerResults;

public class DeleteStickerSwitchKeyboardPacksResult : DeleteStickerCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public int? BotMessageId { get; }
    
    public DeleteStickerSwitchKeyboardPacksResult(long chatId, InlineKeyboardDto keyboardDto, int? botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
}