using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Client;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.TgGateway;

public class TgApiGateway
{
    private ReplyKeyboardMarkup mainKeyboard = new ReplyKeyboardMarkup(new[]
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

    private Client client = new Client();
    
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            await HandleCallbackQuery(botClient, update.CallbackQuery!);
        }

        if (update.Message is not { } message)
            return;

        if (message.Text is not { } messageText)
            return;

        switch (messageText.Split('@').First())
        {
            case "/show_all":
            case "Показать все":
                await HandleWatchAllCommand(botClient, message.Chat.Id, cancellationToken);
                break;
            case "/add_sticker":
            case "Добавить стикер":
                break;
            case "/create_pack":
            case "Создать пак":
                break;
            case "/delete_sticker":
            case "Удалить стикер":
                break;
            case "/delete_pack":
            case "Удалить пак":
                break;
            default:
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Неизвестная команда броу, попробуй чето другое",
                    replyMarkup: mainKeyboard,
                    cancellationToken: cancellationToken);
                break;
        }
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

    private async Task HandleWatchAllCommand(ITelegramBotClient bot, long chatId, CancellationToken ct)
    {
        var command = new SwitchKeyboardCommand(0, 10, "pageright:1");
        var res = client.Handle<SwitchKeyboardResult>(command);
        
        var currentPageKeyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in res.InlineKeyboardDto.Buttons)
            currentPageKeyboard.Add(new[]
                { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in res.InlineKeyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        currentPageKeyboard.Add(lastRow.ToArray());
        var markup = new InlineKeyboardMarkup(currentPageKeyboard.ToArray());
        
        var sentMessage = await bot.SendTextMessageAsync(
            chatId: chatId,
            text: "Вот все ваши стикеры:",
            replyMarkup: markup,
            cancellationToken: ct
        );
    }

    private async Task HandleCallbackQuery(ITelegramBotClient bot, CallbackQuery callback)
    {
        var splitted = callback.Data!.Split(':');
        if (splitted.Length == 1) return;
        
        var command = new SwitchKeyboardCommand(int.Parse(splitted[1]), 10, splitted[0]);
        var res = client.Handle<SwitchKeyboardResult>(command);
        
        var keyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in res.InlineKeyboardDto.Buttons)
            keyboard.Add(new[] { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in res.InlineKeyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        keyboard.Add(lastRow.ToArray());
        var markup = new InlineKeyboardMarkup(keyboard.ToArray());

        

        await bot.EditMessageReplyMarkupAsync(
            chatId: callback.From.Id,
            messageId: callback.Message!.MessageId,
            replyMarkup: markup
        );
    }
}