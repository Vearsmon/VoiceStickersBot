using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;
using VoiceStickersBot.Infra.ObjectStorage;
using VoiceStickersBot.Infra.VSBApplication.Log;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class ShowAllResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.ShowAll;

    private readonly ObjectStorageClient objectStorage = new();
    private readonly ILog log;

    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public ShowAllResultHandler(ILog log)
    {
        this.log = log;
        handlers = new Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>>
        {
            {
                typeof(ShowAllSendStickerResult),
                (bot, infos, res) => Handle(bot, infos, (ShowAllSendStickerResult)res)
            },
            {
                typeof(ShowAllSwitchKeyboardStickersResult),
                (bot, infos, res) => Handle(bot, infos, (ShowAllSwitchKeyboardStickersResult)res)
            },
            {
                typeof(ShowAllSwitchKeyboardPacksResult),
                (bot, infos, res) => Handle(bot, infos, (ShowAllSwitchKeyboardPacksResult)res)
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
        ShowAllSendStickerResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.WaitStickerChoice,
            stickerPackId: result.StickerPackId.ToString());
        
        var memoryStream = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var voiceFile = InputFile.FromStream(memoryStream);
        
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile);

        log.Info("Запрос от {0} на стикер {1}", 
            result.ChatId, result.Sticker.StickerFullId.StickerId);
    }

    private async Task Handle(
        ITelegramBotClient bot, 
        Dictionary<long, UserInfo> userInfos, 
        ShowAllSwitchKeyboardPacksResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        var message = result.HasPacks
            ? "Вот все ваши стикерпаки:"
            : "У вас нет стикерпаков, создайте первый!";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }

    private async Task Handle(
        ITelegramBotClient bot, 
        Dictionary<long, UserInfo> userInfos,
        ShowAllSwitchKeyboardStickersResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitStickerChoice,
            stickerPackId: result.StickerPackId.ToString());
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        var message = "Вот все стикеры из выбранного стикерпака:";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }
}