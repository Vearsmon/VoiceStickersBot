using Amazon.S3;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core.Client;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.Repositories.UsersRepository;
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

    private readonly IUsersRepository userRepository;
    public TgApiGatewayService(
        Client client,
        TgApiCommandService tgApiCommandService,
        TgApiCommandResultHandlerService tgApiCommandResultHandlerService,
        ILog log,
        IUsersRepository userRepository)
    {
        this.client = client;
        this.tgApiCommandService = tgApiCommandService;
        this.tgApiCommandResultHandlerService = tgApiCommandResultHandlerService;
        this.log = log;
        this.userRepository = userRepository;
    }

    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        try
        {
            var chatId = update.Message?.Chat.Id ?? update.CallbackQuery!.Message!.Chat.Id;
            await EnsureAuthenthicated(chatId);
            
            if (update.Type == UpdateType.CallbackQuery)
            {
                var context = BuildQueryContext(update);
                if (context.CommandType == "page") 
                    return;
                
                if (context.CommandType == "AS" && context.CommandStep == "SendInstructions")
                    UserInfoByChatId[context.ChatId] = new UserInfo(
                        UserState.WaitStickerName, 
                        stickerPackId: context.CommandArguments[0]);

                var commandArguments = tgApiCommandService.CreateCommandArguments(context);

                var commandResult = await client.Handle(commandArguments);

                await tgApiCommandResultHandlerService.HandleResult(botClient, commandResult);
            }
            else if (update.Type == UpdateType.Message &&
                     (update.Message!.Voice is not null || update.Message!.Audio is not null))
            {
                var message = update.Message;

                if (!(UserInfoByChatId.TryGetValue(chatId, out var userInfo) && userInfo.State == UserState.WaitFile))
                {
                    await botClient.SendTextMessageAsync(chatId, "Бот не ожидает от вас файла...");
                    return;
                }

                var stickerPackId = userInfo.StickerPackId;
                var stickerName = userInfo.StickerName;
                var fileId = message.Voice == null ? message.Audio!.FileId : message.Voice.FileId;

                var args = new[] { stickerPackId, stickerName, fileId };
                var context = new QueryContext("AS", "AddSticker", args, chatId);

                var command = tgApiCommandService.CreateCommandArguments(context);
                var commandResult = await client.Handle(command);

                await tgApiCommandResultHandlerService.HandleResult(botClient, commandResult);
            }
            else if (update.Type == UpdateType.Message && update.Message!.Text is not null)
            {
                var message = update.Message;
                if (message.Text == "/start" || message.Text == "/cancel")
                {
                    await botClient.SendTextMessageAsync(chatId, "Выберите команду снизу:",
                        replyMarkup: commandsKeyboard);
                }
                else if (UserInfoByChatId.TryGetValue(chatId, out var userInfo) 
                         && userInfo.State == UserState.WaitStickerName)
                    UserInfoByChatId[chatId] = new UserInfo(UserState.WaitFile, userInfo.StickerPackId, message.Text);
                else if (UserInfoByChatId.TryGetValue(chatId, out userInfo) && userInfo.State == UserState.WaitPackName)
                {
                    UserInfoByChatId[chatId] = new UserInfo(UserState.NoWait);

                    var args = new[] { message.Text };
                    var context = new QueryContext("CP", "AddPack", args, chatId);

                    var command = tgApiCommandService.CreateCommandArguments(context);
                    var commandResult = await client.Handle(command);

                    await tgApiCommandResultHandlerService.HandleResult(botClient, commandResult);
                }
                else
                {
                    var context = QueryContextByCommand[message.Text](chatId);

                    if (message.Text == "Создать пак")
                        UserInfoByChatId[chatId] = new UserInfo(UserState.WaitPackName);
                    else if (message.Text == "Добавить стикер")
                        UserInfoByChatId[chatId] = new UserInfo(
                            UserState.WaitStickerName, 
                            stickerPackId: context.CommandArguments[0]);

                    var command = tgApiCommandService.CreateCommandArguments(context);
                    var commandResult = await client.Handle(command);

                    await tgApiCommandResultHandlerService.HandleResult(botClient, commandResult);
                }
            }
        }
        catch (Exception e)
        {
            log.Error(e, "Appppp crashed");
            var chatId = update.Message?.Chat.Id ?? update.CallbackQuery!.Message!.Chat.Id;
            UserInfoByChatId[chatId] = new UserInfo(UserState.NoWait);
        }
    }

    private readonly Dictionary<long, UserInfo> UserInfoByChatId = new();

    private static QueryContext BuildQueryContext(Update update)
    {
        var callbackData = update.CallbackQuery!.Data!.Split(
            ':', 
            StringSplitOptions.RemoveEmptyEntries);
        
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
        {
            "Показать все", chatId => new QueryContext(
                "SA", "SwKbdPc", new[] { "0", "Increase", "10" }, chatId)
        },
        {
            "Добавить стикер", chatId => new QueryContext(
                "AS", "SwKbdPc", new[] { "0", "Increase", "10" }, chatId)
        },
        {
            "Создать пак", chatId => new QueryContext(
                "CP", "SendInstructions", Array.Empty<string>(), chatId)
        },
        {
            "Удалить стикер", chatId => new QueryContext(
                "DS", "SwKbdPc", new[] { "0", "Increase", "10" }, chatId)
        },
        {
            "Удалить пак", chatId => new QueryContext(
                "DP", "SwKbdPc", new[] { "0", "Increase", "10" }, chatId)
        }
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

    private async Task EnsureAuthenthicated(long chatId)
    {
        await userRepository.CreateIfNotExists(chatId.ToString());
    }
}