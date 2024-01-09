using Telegram.Bot.Types.ReplyMarkups;

namespace VoiceStickersBot.TgGateway;

public static class Keyboards
{
    public static readonly ReplyKeyboardMarkup DialogKeyboard = new ReplyKeyboardMarkup(new[]
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
        },
        new[] // fourth row
        {
            new KeyboardButton("Импорт/экспорт пака"),
        }
    }) { ResizeKeyboard = true };
    
    public static readonly ReplyKeyboardMarkup GroupKeyboard = new ReplyKeyboardMarkup(new[]
    {
        new[]
        {
            new KeyboardButton("Показать все")
        },
        new[]
        {
            new KeyboardButton("Удалить пак"),
        },
        new[]
        {
            new KeyboardButton("Импорт/экспорт пака"),
        }
    }) { ResizeKeyboard = true };
}