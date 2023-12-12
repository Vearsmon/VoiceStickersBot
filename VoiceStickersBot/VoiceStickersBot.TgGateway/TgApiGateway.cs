using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Client;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.CommandsFactory;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;
using VoiceStickersBot.TgGateway.CommandResultHandlers;

namespace VoiceStickersBot.TgGateway;

public class TgApiGateway
{
    private Dictionary<long, bool> userWait = new ();
    
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

    private TgApiCommandService commandService =
        new (new List<ICommandFactory>() { new SwitchKeyboardCommandFactory() });

    private TgApiCommandResultCallbackHandlerService resultCallbackHandlerService = new(
        new List<ICommandResultHandler>() { new SwitchKeyboardResultHandler() }
        );
    
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            var callbackData = update.CallbackQuery!.Data!;
            var callbackMsg = update.CallbackQuery!.Message!;
            var command = commandService.CreateInlineCommand(new CommandObject(callbackData, new RequestContex(callbackMsg.Chat.Id)));
            var commandResult = client.Handle(command);
            await resultCallbackHandlerService.HandleWithCallback(botClient, commandResult, update.CallbackQuery);
        }
        else if (update.Type == UpdateType.Message)
        {
            var message = update.Message;
            var command =
                commandService.CreateInlineCommand(new CommandObject(message.Text, new RequestContex(message.Chat.Id)));
            var commandResult = client.Handle(command);
            await resultCallbackHandlerService.HandleWithMessage(botClient, commandResult, message);
        }
        //Кароче щас не очень хорошо работает только кнопки переключение страниц работают на старых отправленных
        //сообщениях. Надо замутить вложенные команды както, видимо послезавтра займусь. И уже можно начинать
        //реализовывать логику всех кнопочек по аналогии с готовым.

        /*if (userWait.TryGetValue(message.Chat.Id, out var isWaiting) && isWaiting)
        {
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Получил месагу без напрягу",
                replyMarkup: mainKeyboard,
                cancellationToken: cancellationToken);
            userWait[message.Chat.Id] = false;
        }
        else
        {
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Начинаю ждать",
                replyMarkup: mainKeyboard,
                cancellationToken: cancellationToken);
            userWait[message.Chat.Id] = true;
        }*/
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
}