using Telegram.Bot.Types.ReplyMarkups;

namespace VoiceStickersBot.TgGateway;

public static class DefaultKeyboard
{
    public static readonly ReplyKeyboardMarkup CommandsKeyboard = new ReplyKeyboardMarkup(new[]
    {
        new[] // first row
        {
            new KeyboardButton("Показать все")
        },
        new[] // second row
        {
            new KeyboardButton("Добавить стикер"),
            new KeyboardButton("Создать пак")
        },
        new[] // third row
        {
            new KeyboardButton("Удалить стикер"),
            new KeyboardButton("Удалить пак")
        }
    }) { ResizeKeyboard = true };
}