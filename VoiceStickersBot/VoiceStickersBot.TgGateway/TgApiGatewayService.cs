using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Core.Client;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.Repositories.UsersRepository;
using VoiceStickersBot.Infra.VSBApplication.Log;
using VoiceStickersBot.TgGateway.CommandResultHandlers;

namespace VoiceStickersBot.TgGateway;

// ReSharper disable once ClassNeverInstantiated.Global
public class TgApiGatewayService
{
    private readonly Client client;

    private readonly TgApiCommandService tgApiCommandService;
    private readonly TgApiCommandResultHandlerService tgApiCommandResultHandlerService;
    
    private readonly Dictionary<long, UserInfo> userInfoByChatId = new();
    
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
        var chatId = update.Message?.Chat.Id ?? update.CallbackQuery!.Message!.Chat.Id;
        try
        {
            await EnsureAuthenthicated(chatId);
            
            QueryContext context;
            
            if (update.Type == UpdateType.CallbackQuery)
            {
                context = BuildQueryContext(update);
                if (context.CommandType == "page") 
                    return;

                if (userInfoByChatId.TryGetValue(chatId, out var userInfo)
                    && (context.CommandStep == "SendSticker" || context.CommandStep == "DeleteSticker"))
                {
                    context.CommandArguments.Add(userInfo.StickerPackId);
                }
            }
            else if (update.Type == UpdateType.Message &&
                     (update.Message!.Voice is not null || update.Message!.Audio is not null))
            {
                var message = update.Message;
                
                if (!(userInfoByChatId.TryGetValue(chatId, out var userInfo) && userInfo.State == UserState.WaitFile))
                {
                    await botClient.SendTextMessageAsync(
                        chatId,
                        "Бот не ожидает от вас файла...",
                        replyMarkup: DefaultKeyboard.CommandsKeyboard);
                    return;
                }
                
                /*if (message.Audio is not null && message.Audio?.MimeType != "audio/x-opus+ogg")
                {
                    await botClient.SendTextMessageAsync(chatId, "Расширение файла должно быть .opus",
                        replyMarkup: DefaultKeyboard.CommandsKeyboard);
                    return;
                }*/

                var stickerPackId = userInfo.StickerPackId;
                var stickerName = userInfo.StickerName;
                var fileId = message.Voice == null ? message.Audio!.FileId : message.Voice.FileId;

                var args = new List<string> { stickerPackId, stickerName, fileId, message.Audio?.MimeType! };
                context = new QueryContext("AS", "AddSticker", args, chatId);
            }
            else if (update.Type == UpdateType.Message && update.Message!.Text is not null)
            {
                var message = update.Message;
                
                if (QueryContextByCommand.ContainsKey(message.Text))
                    context = QueryContextByCommand[message.Text](chatId);
                
                else if (userInfoByChatId.TryGetValue(chatId, out var userInfo) 
                         && userInfo.State == UserState.WaitStickerName)
                {
                    var stickerPackId = userInfo.StickerPackId;
                    var args = new List<string> { stickerPackId, message.Text };
                    context = new QueryContext("AS", "SendFileInstr", args, chatId);
                }
                else if (userInfoByChatId.TryGetValue(chatId, out userInfo) && userInfo.State == UserState.WaitPackName)
                {
                    var args = new List<string> { message.Text };
                    context = new QueryContext("CP", "AddPack", args, chatId);
                }
                else if (userInfoByChatId.TryGetValue(chatId, out userInfo) && userInfo.State == UserState.WaitPackId)
                {
                    var args = new List<string> { message.Text };
                    context = new QueryContext("SP", "ImportPack", args, chatId);
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId, 
                        "Неизвестная команда, попробуйте выбрать команду из меню",
                        replyMarkup: DefaultKeyboard.CommandsKeyboard);
                    return;
                }
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, "Неизвестная команда, попробуйте выбрать команду из меню",
                        replyMarkup: DefaultKeyboard.CommandsKeyboard);
                return;
            }
            var command = tgApiCommandService.CreateCommandArguments(context);
            var commandResult = await client.Handle(command);

            await tgApiCommandResultHandlerService.HandleResult(botClient, userInfoByChatId, commandResult);
        }
        catch (Exception e)
        {
            log.Error(e, "Appppp crashed");
            await botClient.SendTextMessageAsync(chatId, "Что-то пошло не так... " +
                                                         "Возможно, сообщение уже не актуально");
            userInfoByChatId[chatId] = new UserInfo(UserState.NoWait);
        }
    }


    private static QueryContext BuildQueryContext(Update update)
    {
        var callbackData = update.CallbackQuery!.Data!.Split(
            ':', 
            StringSplitOptions.RemoveEmptyEntries);
        
        var callbackMsg = update.CallbackQuery!.Message!;
        var chatId = callbackMsg.Chat.Id;
        var commandType = callbackData[0];
        var commandStep = callbackData[1];
        var callbackArguments = callbackData.Skip(2).ToList();
        var botMessageId = update.CallbackQuery.Message!.MessageId;

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
                "SA", "SwKbdPc", new() { "0", "Increase", "10" }, chatId)
        },
        {
            "Добавить стикер", chatId => new QueryContext(
                "AS", "SwKbdPc", new() { "0", "Increase", "10" }, chatId)
        },
        {
            "Создать пак", chatId => new QueryContext(
                "CP", "SendInstructions", new(), chatId)
        },
        {
            "Удалить стикер", chatId => new QueryContext(
                "DS", "SwKbdPc", new() { "0", "Increase", "10" }, chatId)
        },
        {
            "Удалить пак", chatId => new QueryContext(
                "DP", "SwKbdPc", new() { "0", "Increase", "10" }, chatId)
        },
        {
            "Импорт/экспорт пака", chatId => new QueryContext(
                "SP", "Choice", new(), chatId)
        },
        {
            "/cancel", chatId => new QueryContext(
                "Cancel", "Cancel", new(), chatId)
        },
        {
            "/start", chatId => new QueryContext(
                "Start", "Start", new(), chatId)
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