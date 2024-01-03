using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSwitchKeyboardStickersResult : ICommandResult
{
    public long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public string BotMessageId { get; }
    public AddStickerSwitchKeyboardStickersResult(
        long chatId, 
        InlineKeyboardDto keyboardDto, 
        string botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
}