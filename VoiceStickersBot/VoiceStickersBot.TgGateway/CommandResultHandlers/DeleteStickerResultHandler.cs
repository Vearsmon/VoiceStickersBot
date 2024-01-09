using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeleteStickerResults;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class DeleteStickerResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.DeleteSticker;
    
    private readonly ObjectStorageClient objectStorage = new();
    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public DeleteStickerResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>>()
        {
            {
                typeof(DeleteStickerSwitchKeyboardPacksResult),
                async (bot, infos, res) => await Handle(bot, infos, (DeleteStickerSwitchKeyboardPacksResult)res)
            },
            {
                typeof(DeleteStickerSwitchKeyboardStickersResult),
                async (bot, infos, res) => await Handle(bot, infos, (DeleteStickerSwitchKeyboardStickersResult)res)
            },
            {
                typeof(DeleteStickerSendStickerResult),
                async (bot, infos, res) => await Handle(bot, infos, (DeleteStickerSendStickerResult)res)
            },
            {
                typeof(DeleteStickerDeleteStickerResult),
                async (bot, infos, res) => await Handle(bot, infos, (DeleteStickerDeleteStickerResult)res)
            }
        };
    }
    public Task HandleResult(ITelegramBotClient bot, Dictionary<long, UserInfo> userInfos, ICommandResult result)
    {
        return handlers[result.GetType()](bot, userInfos, result);
    }
    
    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        DeleteStickerDeleteStickerResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);
        
        var keyboard = Keyboards.DialogKeyboard;
        
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Стикер успешно удалён",
            replyMarkup: keyboard);
    }
    
    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        DeleteStickerSwitchKeyboardPacksResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);

        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        var message = "Выберите набор, из которого хотите удалить стикер:";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        DeleteStickerSwitchKeyboardStickersResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitStickerChoice,
            stickerPackId: result.StickerPackId.ToString());
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        var message = "Выберите стикер, который хотите удалить:";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        DeleteStickerSendStickerResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitStickerChoice,
            stickerPackId: result.StickerPackId.ToString());
        
        var memoryStream = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var voiceFile = InputFile.FromStream(memoryStream);
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile);

        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Вы точно хотите удалить этот стикер?",
            replyMarkup: markup);

        await bot.DeleteMessageAsync(result.ChatId, result.BotMessageId!.Value);
    }
}

