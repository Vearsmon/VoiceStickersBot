using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core.Client;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Infra.VSBApplication.Log;
using VoiceStickersBot.TgGateway.CommandResultHandlers;

namespace VoiceStickersBot.TgGateway;

// ReSharper disable once ClassNeverInstantiated.Global
public class TgApiGatewayService
{
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

    private readonly Client client;

    private readonly TgApiCommandService tgApiCommandService;
    private readonly TgApiCommandResultHandlerService tgApiCommandResultHandlerService;

    private readonly ILog log;

    public TgApiGatewayService(
        Client client,
        TgApiCommandService tgApiCommandService,
        TgApiCommandResultHandlerService tgApiCommandResultHandlerService,
        ILog log)
    {
        this.client = client;
        this.tgApiCommandService = tgApiCommandService;
        this.tgApiCommandResultHandlerService = tgApiCommandResultHandlerService;
        this.log = log;
    }

    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            var context = BuildQueryContext(update);

            var commandArguments = tgApiCommandService.CreateCommandArguments(context);
            var commandResult = await client.Handle(commandArguments);

            await tgApiCommandResultHandlerService.HandleResult(botClient, commandResult);
        }
        else if (update.Type == UpdateType.Message && 
                 (update.Message!.Voice is not null || update.Message!.Audio is not null))
        {
            // обсудить чем лучше ебнуть - строкой, енумом или бля че
            //if userState == waitFile
            //else "Wrong command Try again..."
            
            var message = update.Message;
            var chatId = message!.Chat.Id;
            var stickerPackId = ""; // TODO: словарик с состояниями в формате userId: "WaitFile:stickerPackId"
            var stickerName = message.Caption!; //if text == null bot.send("Братан по русски
                                                //же написал с названием отправляй"), Надо узнать как капшн к голосовым
                                                // делать либо если нет подписи, то просто name==id
                                                // вот это вообще тугой момент намекающий на болото ифов, пахнет залупами
            var fileId = message.Voice == null ? message.Audio!.FileId : message.Voice.FileId;
            
            var args = new[] { stickerPackId, stickerName, fileId, $"{chatId}" };
            var context = new QueryContext("AS", "AddSticker", args, chatId);

            var command = tgApiCommandService.CreateCommandArguments(context);
            var commandResult = await client.Handle(command);

            await tgApiCommandResultHandlerService.HandleResult(botClient, commandResult);
        }
        else if (update.Type == UpdateType.Message && update.Message!.Text is not null)
        {
            var message = update.Message;
            var chatId = message!.Chat.Id;

            var context = QueryContextByCommand[message.Text](chatId);

            var command = tgApiCommandService.CreateCommandArguments(context);
            var commandResult = await client.Handle(command);

            await tgApiCommandResultHandlerService.HandleResult(botClient, commandResult);
        }
    }

    private static QueryContext BuildQueryContext(Update update)
    {
        var callbackData = update.CallbackQuery!.Data!.Split(':');
        var callbackMsg = update.CallbackQuery!.Message!;
        var chatId = callbackMsg.Chat.Id;
        var commandType = callbackData[0];
        var commandStep = callbackData[1];
        var callbackArguments = callbackData.Skip(2).ToArray();
        var botMessageId = update.CallbackQuery.InlineMessageId!;

        return new QueryContext(
            commandType,
            commandStep,
            callbackArguments,
            chatId,
            botMessageId);
    }

    private static Dictionary<string, Func<long, QueryContext>> QueryContextByCommand = new()
    {
        {"Показать все", chatId => new QueryContext(
            "SA", "SwKbdPc", new[] { $"{chatId}", "0", "Increase", "10" }, chatId
            )},
        {"Добавить стикер", chatId => new QueryContext(
            "AS", "SwKbdPc", new[] { $"{chatId}", "0", "Increase", "10" }, chatId
            )},
        {"Создать пак", chatId => new QueryContext(
            "CP", "SendInstructions", new[] { $"{chatId}" }, chatId
            )},
        {"Удалить стикер", chatId => new QueryContext(
            "DS", "SwKbdPc", new[] { $"{chatId}", "0", "Increase", "10" }, chatId
            )},
        {"Удалить пак", chatId => new QueryContext(
            "DP", "SwKbdPc", new[] { $"{chatId}", "0", "Increase", "10" }, chatId
            )}
    };

    public Task HandlePollingErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}