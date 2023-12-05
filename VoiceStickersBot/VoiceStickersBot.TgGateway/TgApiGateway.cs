using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.TgGateway;

public class TgApiGateway
{
    private ReplyKeyboardMarkup mainKeyboard = new ReplyKeyboardMarkup(new[]
    {
        new[] // first row
        {
            new KeyboardButton("Посмотреть все")
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

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            await HandleCallbackQuery(botClient, update.CallbackQuery!, update.CallbackQuery!.Data![^1]);
        }

        if (update.Message is not { } message)
            return;

        if (message.Text is not { } messageText)
            return;

        switch (messageText)
        {
            case "Посмотреть все":
                await HandleWatchAllCommand(botClient, message.Chat.Id, cancellationToken);
                break;
            case "Добавить стикер":
                break;
            case "Создать пак":
                break;
            case "Удалить стикер":
                break;
            case "Удалить пак":
                break;
            default:
                break;
        }

        var chatId = message.Chat.Id;

        var sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: messageText,
            replyMarkup: mainKeyboard,
            cancellationToken: cancellationToken);
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
        var handler = new SwitchKeyboardHandler(command);
        var res = (SwitchKeyboardResult)handler.Handle();
        var currentPageKeyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in res.InlineKeyboardDto.Buttons)
            currentPageKeyboard.Add(new[]
                { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in res.InlineKeyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        currentPageKeyboard.Add(lastRow.ToArray());
        var markup = new InlineKeyboardMarkup(currentPageKeyboard.ToArray());

        //var currentPageKeyboard = SwitchKeyboardPage(allPacks, 10, 0, PageChangeType.Increase);
        var sentMessage = await bot.SendTextMessageAsync(
            chatId: chatId,
            text: "Вот все ваши стикеры:",
            replyMarkup: markup,
            cancellationToken: ct
        );
    }

    private InlineKeyboardMarkup SwitchKeyboardPage(string[] packs, int count, int pageFrom, PageChangeType pcs)
    {
        var pageTo = pcs == PageChangeType.Increase ? pageFrom + 1 : pageFrom - 1;
        var startIndex = pcs == PageChangeType.Increase ? (pageTo - 1) * count : (pageFrom - 2) * count;
        var endIndex = pcs == PageChangeType.Increase ? count * (pageFrom + 1) : pageTo * count;

        var keyboard = new List<InlineKeyboardButton[]>();
        for (var i = startIndex; i < packs.Length && i < endIndex; i++)
            keyboard.Add(new[] { InlineKeyboardButton.WithCallbackData(packs[i], "pack_id") });

        var lastLineButtons = new List<InlineKeyboardButton>();
        if (pageTo > 1)
            lastLineButtons.Add(InlineKeyboardButton.WithCallbackData("\u25c0\ufe0f", $"pageleft:{pageTo}"));

        lastLineButtons.Add(InlineKeyboardButton.WithCallbackData($"{pageTo}", "pagenum"));

        if (pageTo <= packs.Length / count)
            lastLineButtons.Add(InlineKeyboardButton.WithCallbackData("\u25b6\ufe0f", $"pageright:{pageTo}"));

        keyboard.Add(lastLineButtons.ToArray());

        return new InlineKeyboardMarkup(keyboard.ToArray());
    }

    private async Task HandleCallbackQuery(ITelegramBotClient bot, CallbackQuery callback, int pageFrom)
    {
        var splitted = callback.Data!.Split(':');
        if (splitted.Length == 1) return;
        var command = new SwitchKeyboardCommand(int.Parse(splitted[1]), 10, splitted[0]);
        //var client = new Client();
        var handler = new SwitchKeyboardHandler(command);
        var res = (SwitchKeyboardResult)handler.Handle();
        var keyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in res.InlineKeyboardDto.Buttons)
            keyboard.Add(new[] { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in res.InlineKeyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        keyboard.Add(lastRow.ToArray());
        var markup = new InlineKeyboardMarkup(keyboard.ToArray());

        //var lastRow = new List<InlineKeyboardButton>();
        //keyboard.Add(new []{});
        //var keyboard = client.Obrabotat(command);

        await bot.EditMessageReplyMarkupAsync(
            chatId: callback.From.Id,
            messageId: callback.Message!.MessageId,
            replyMarkup: markup
        );
    }
}