using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class ShowAllResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.ShowAll;

    private readonly ObjectStorageClient objectStorage = new();

    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public ShowAllResultHandler()
    {
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
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);
        
        var memoryStream = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var voiceFile = InputFile.FromStream(memoryStream);
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile,
            replyMarkup: DefaultKeyboard.CommandsKeyboard);
    }

    private async Task Handle(
        ITelegramBotClient bot, 
        Dictionary<long, UserInfo> userInfos, 
        ShowAllSwitchKeyboardPacksResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        if (result.BotMessageId is null)
        {
            await bot.SendTextMessageAsync(
                result.ChatId,
                "Вот все ваши наборы:",
                replyMarkup: markup);
        }
        else
        {
            await bot.EditMessageReplyMarkupAsync(
                inlineMessageId: result.BotMessageId,
                replyMarkup: markup);
        }
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

        if (result.BotMessageId is null)
        {
            var msg = await bot.SendTextMessageAsync(
                result.ChatId,
                "Вот все стикеры из выбранного набора:",
                replyMarkup: markup);
        }
        else
        {
            await bot.EditMessageReplyMarkupAsync(
                inlineMessageId: result.BotMessageId,
                replyMarkup: markup);
        }
    }
}