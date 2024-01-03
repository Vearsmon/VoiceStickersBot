using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class SwitchKeyboardResultExtensions
{
    public static InlineKeyboardMarkup GetMarkupFromDto(InlineKeyboardDto keyboardDto)
    {
        var currentPageKeyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in keyboardDto.Buttons)
            currentPageKeyboard.Add(new[]
                { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in keyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        currentPageKeyboard.Add(lastRow.ToArray());

        return new InlineKeyboardMarkup(currentPageKeyboard);
    }
}