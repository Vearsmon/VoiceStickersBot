using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;

namespace VoiceStickersBot.Core.CommandResults.ShowAllResults;

public class ShowAllSwitchKeyboardPacksResult : ISwitchKeyboardResult
{
    public long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public string BotMessageId { get; }

    
    public ShowAllSwitchKeyboardPacksResult(long chatId, InlineKeyboardDto keyboardDto, string botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
    
}