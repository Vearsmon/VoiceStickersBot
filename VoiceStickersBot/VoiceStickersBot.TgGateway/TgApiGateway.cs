using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Client;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.CommandsFactory;
using VoiceStickersBot.TgGateway.CommandResultHandlers;

namespace VoiceStickersBot.TgGateway;

public class TgApiGateway
{
    private Dictionary<long, UserBotState> userStates = new ();
    
    private ReplyKeyboardMarkup commandsKeyboard = new ReplyKeyboardMarkup(new[]
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
        new (new List<ICommandFactory>() 
            { 
                new SwitchKeyboardCommandFactory(),
                new ShowAllCommandFactory(), 
                new AddStickerCommandFactory() 
            }
        );

    private TgApiCommandResultCallbackHandlerService resultHandlerService = 
        new(new List<ICommandResultHandler>()
            {
                new SwitchKeyboardResultHandler(), 
                new ShowAllResultHandler(),
                new AddStickerResultHandler()
            }
        );
    
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            var callbackData = update.CallbackQuery!.Data!;
            var callbackMsg = update.CallbackQuery!.Message!;
            var chatId = callbackMsg.Chat.Id;
            var currentState = userStates.GetValueOrDefault(chatId, UserBotState.WaitCommand);
            Console.WriteLine($"was {chatId}:{currentState}");
            var context = new RequestContext(callbackData, chatId, currentState);
            var command = commandService.CreateInlineCommand(context);
            var commandResult = client.Handle(command);
            userStates[chatId] = 
                await resultHandlerService.HandleFromCallback(botClient, commandResult, update.CallbackQuery);
            Console.WriteLine($"now {chatId}:{userStates[chatId]}");
        }
        else if (update.Type == UpdateType.Message && update.Message.Text is not null)
        {
            var message = update.Message;
            var chatId = message!.Chat.Id;
            var currentState = userStates.GetValueOrDefault(chatId, UserBotState.WaitCommand);
            Console.WriteLine($"was {chatId}:{currentState}");
            var context = new RequestContext(message.Text, chatId, currentState);
            var command = commandService.CreateTextCommand(context);
            var commandResult = client.Handle(command);
            userStates[chatId] =
                await resultHandlerService.HandleFromMessage(botClient, commandResult, message);
            Console.WriteLine($"now {chatId}:{userStates[chatId]}");
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