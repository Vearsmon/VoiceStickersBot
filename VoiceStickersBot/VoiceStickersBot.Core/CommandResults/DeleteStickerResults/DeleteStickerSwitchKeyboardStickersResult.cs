using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.DeleteStickerResults;

public class DeleteStickerSwitchKeyboardStickersResult : DeleteStickerCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public Guid StickerPackId { get; }
    public int? BotMessageId { get; }

    public DeleteStickerSwitchKeyboardStickersResult(
        long chatId,
        InlineKeyboardDto keyboardDto,
        Guid stickerPackId,
        int? botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        StickerPackId = stickerPackId;
        BotMessageId = botMessageId;
    }
}