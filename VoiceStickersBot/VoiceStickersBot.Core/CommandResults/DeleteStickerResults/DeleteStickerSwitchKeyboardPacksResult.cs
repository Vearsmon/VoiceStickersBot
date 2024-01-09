using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.DeleteStickerResults;

public class DeleteStickerSwitchKeyboardPacksResult : DeleteStickerCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public bool HasPacks { get; }
    public int? BotMessageId { get; }
    
    public DeleteStickerSwitchKeyboardPacksResult(
        long chatId,
        InlineKeyboardDto keyboardDto,
        bool hasPacks,
        int? botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
        HasPacks = hasPacks;
    }
}