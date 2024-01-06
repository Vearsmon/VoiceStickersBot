using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSwitchKeyboardStickersResult : AddStickerCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public Guid StickerPackId { get; }
    public string BotMessageId { get; }

    public AddStickerSwitchKeyboardStickersResult(
        long chatId,
        InlineKeyboardDto keyboardDto,
        Guid stickerPackId,
        string botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        StickerPackId = stickerPackId;
        BotMessageId = botMessageId;
    }
}