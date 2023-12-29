using Amazon.Runtime.Internal;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Client;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.CommandsFactory;
using VoiceStickersBot.Core.CommandsObsolete.CommandsFactoryObsolete;
using VoiceStickersBot.Core.CommandArguments;
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

    private readonly Client client = new Client();

    private TgApiCommandService commandService =
        new (new List<ICommandArgumentsFactory>() 
            { 
                new ShowAllCommandArgumentsFactory()
            }
        );

    private TgApiCommandResultHandlerService resultHandlerService = 
        new(new List<ICommandResultHandler>()
            { 
                new ShowAllResultHandler(),
            }
        );
    
    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient, 
        Update update,
        CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            var callbackData = update.CallbackQuery!.Data!.Split(':');
            var callbackMsg = update.CallbackQuery!.Message!;
            var chatId = callbackMsg.Chat.Id;
            var commandType = callbackData[0];
            var commandStep = callbackData[1];
            var callbackArguments = callbackData.Skip(2).ToArray();
            var botMessageId = callbackMsg.MessageId;
            
            var context = new QueryContext(
                commandType, 
                commandStep, 
                callbackArguments,
                chatId, 
                botMessageId.ToString());
            
            var commandArguments = commandService.CreateCommandArguments(context);
            var commandResult = await client.Handle(commandArguments);
            await resultHandlerService.HandleResult(botClient, commandResult);
        }
        else if (update.Type == UpdateType.Message && update.Message.Text is not null)
        {
            var message = update.Message;
            var chatId = message!.Chat.Id;

            var args = new[] { $"{chatId}",  "1", "Increase", "10" };
            var context = new QueryContext("Показать все", "SwitchKeyboardPacks", args, chatId);
            var command = commandService.CreateCommandArguments(context);
            var commandResult = await client.Handle(command);
            await resultHandlerService.HandleResult(botClient, commandResult);
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
}