using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.CommandResults.DeleteStickerResults;

public class DeleteStickerSendStickerResult : DeleteStickerCommandResultBase
{
    public override long ChatId { get; }
    public Sticker Sticker { get; }
    public Guid StickerPackId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public int? BotMessageId { get; }

    public DeleteStickerSendStickerResult(
        long chatId,
        Sticker sticker,
        Guid stickerPackId,
        InlineKeyboardDto keyboardDto,
        int? botMessageId)
    {
        ChatId = chatId;
        Sticker = sticker;
        StickerPackId = stickerPackId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
}