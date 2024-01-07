using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackSwitchKeyboardStickersResult : SharePackCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public Guid StickerPackId { get; }
    public string BotMessageId { get; }

    public SharePackSwitchKeyboardStickersResult(
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