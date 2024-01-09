using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSwitchKeyboardPacksResult : AddStickerCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public bool HasPacks { get; }
    public int? BotMessageId { get; }

    public AddStickerSwitchKeyboardPacksResult(
        long chatId,
        InlineKeyboardDto keyboardDto,
        bool hasPacks,
        int? botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        HasPacks = hasPacks;
        BotMessageId = botMessageId;
    }
}