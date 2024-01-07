using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class AddStickerResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private readonly ObjectStorageClient objectStorage = new();

    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public AddStickerResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>>
        {
            {
                typeof(AddStickerAddStickerResult),
                async (bot, infos, res) => await Handle(bot, infos, (AddStickerAddStickerResult)res)
            },
            {
                typeof(AddStickerSendStickerResult),
                (bot, infos, res) => Handle(bot, infos, (AddStickerSendStickerResult)res)
            },
            {
                typeof(AddStickerSwitchKeyboardPacksResult),
                (bot, infos, res) => Handle(bot, infos, (AddStickerSwitchKeyboardPacksResult)res)
            },
            {
                typeof(AddStickerSwitchKeyboardStickersResult),
                (bot, infos, res) => Handle(bot, infos, (AddStickerSwitchKeyboardStickersResult)res)
            },
            {
                typeof(AddStickerSendNameInstructionsResult),
                (bot, infos, res) => Handle(bot, infos, (AddStickerSendNameInstructionsResult)res)
            },
            {
                typeof(AddStickerSendFileInstructionsResult),
                (bot, infos, res) => Handle(bot, infos, (AddStickerSendFileInstructionsResult)res)
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
        AddStickerAddStickerResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);
        
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Стикер успешно добавлен",
            replyMarkup: DefaultKeyboard.CommandsKeyboard);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        AddStickerSendStickerResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitStickerChoice,
            stickerPackId: result.StickerPackId.ToString());
        
        var memoryStream = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var voiceFile = InputFile.FromStream(memoryStream);
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        AddStickerSendNameInstructionsResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitStickerName,
            stickerPackId: result.StickerPackId.ToString());
        
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Отправьте название будущего стикера");
    }
    
    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        AddStickerSendFileInstructionsResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitFile,
            stickerPackId: result.StickerPackId.ToString(),
            stickerName: result.StickerName);
        
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Теперь отправьте аудиофайл или голосовое сообщение");
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        AddStickerSwitchKeyboardPacksResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);
    
        var message = "Выберите стикерпак, в который хотите добавить стикер:";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        AddStickerSwitchKeyboardStickersResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitStickerChoice,
            stickerPackId: result.StickerPackId.ToString());
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        var message = "Вот все стикеры из выбранного набора:";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }
}