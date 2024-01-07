using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class SwitchKeyboardResultExtensions
{
    public static InlineKeyboardMarkup GetMarkupFromDto(InlineKeyboardDto keyboardDto)
    {
        var resultKeyboard = new List<InlineKeyboardButton[]>();
        
        foreach (var buttonRow in keyboardDto.ButtonsRows)
        {
            var currentRow = new List<InlineKeyboardButton>();
            foreach (var button in buttonRow)
                currentRow.Add(InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData));
            
            resultKeyboard.Add(currentRow.ToArray());
        }
        
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in keyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        resultKeyboard.Add(lastRow.ToArray());

        return new InlineKeyboardMarkup(resultKeyboard);
    }
}