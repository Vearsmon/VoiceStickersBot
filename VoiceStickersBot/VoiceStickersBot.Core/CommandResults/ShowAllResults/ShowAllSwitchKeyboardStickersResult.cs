using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;

namespace VoiceStickersBot.Core.CommandResults.ShowAllResults;

public class ShowAllSwitchKeyboardStickersResult : ISwitchKeyboardResult
{
    public long ChatId { get; }
    public string BotMessageId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    
    public ShowAllSwitchKeyboardStickersResult(long chatId, InlineKeyboardDto keyboardDto, string botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
}