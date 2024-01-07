using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.DeletePackResults;

public class DeletePackSwitchKeyboardStickersResult : DeletePackCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public Guid StickerPackId { get; }
    public string BotMessageId { get; }

    public DeletePackSwitchKeyboardStickersResult(
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