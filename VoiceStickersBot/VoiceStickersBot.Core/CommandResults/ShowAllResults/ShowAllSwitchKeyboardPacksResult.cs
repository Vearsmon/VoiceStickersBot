using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandResults.ShowAllResults;

public class ShowAllSwitchKeyboardPacksResult : ISwitchKeyboardResult
{
    public long ChatId { get; }
    public string BotMessageId { get; }
    public InlineKeyboardDto KeyboardDto { get; }

    
    public ShowAllSwitchKeyboardPacksResult(long chatId, InlineKeyboardDto keyboardDto, string botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
    
}