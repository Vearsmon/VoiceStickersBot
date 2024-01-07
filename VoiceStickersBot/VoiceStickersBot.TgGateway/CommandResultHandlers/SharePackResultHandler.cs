using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.SharePackResults;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class SharePackResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.SharePack;

    private readonly ObjectStorageClient objectStorage = new();

    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public SharePackResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>>
        {
            {
                typeof(SharePackSendStickerResult),
                (bot, infos, res) => Handle(bot, infos, (SharePackSendStickerResult)res)
            },
            {
                typeof(SharePackSwitchKeyboardPacksResult),
                (bot, infos, res) => Handle(bot, infos, (SharePackSwitchKeyboardPacksResult)res)
            },
            {
                typeof(SharePackSwitchKeyboardStickersResult),
                (bot, infos, res) => Handle(bot, infos, (SharePackSwitchKeyboardStickersResult)res)
            },
            {
                typeof(SharePackChoiceResult),
                async (bot, infos, res) => await Handle(bot, infos, (SharePackChoiceResult)res)
            },
            {
                typeof(SharePackSendImportInstructionsResult),
                (bot, infos, res) => Handle(bot, infos, (SharePackSendImportInstructionsResult)res)
            },
            {
                typeof(SharePackImportPackResult),
                (bot, infos, res) => Handle(bot, infos, (SharePackImportPackResult)res)
            }
            ,
            {
                typeof(SharePackSendPackIdResult),
                (bot, infos, res) => Handle(bot, infos, (SharePackSendPackIdResult)res)
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
        SharePackSwitchKeyboardPacksResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        var message = "Выберите стикерпак, которым хотите поделиться:";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        SharePackSwitchKeyboardStickersResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitStickerChoice,
            stickerPackId: result.StickerPackId.ToString());
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        var message = "Вот все стикеры из выбранного набора:";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        SharePackSendStickerResult result)
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
        SharePackImportPackResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);
        if (result.IsSucceeded)
            await bot.SendTextMessageAsync(
                result.ChatId,
                "Пак успешно импортирован",
                replyMarkup: DefaultKeyboard.CommandsKeyboard);
        else
            await bot.SendTextMessageAsync(
                result.ChatId,
                "Ошибка при импорте пака, попробуйте ещё раз",
                replyMarkup: DefaultKeyboard.CommandsKeyboard);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        SharePackSendImportInstructionsResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitPackId);
        
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Отправьте ID стикерпака, который вам прислал другой пользователь");
    }
    
    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        SharePackSendPackIdResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.NoWait);

        await bot.SendTextMessageAsync(
            parseMode: ParseMode.Markdown,
            chatId: result.ChatId,
            text: $"```{result.StickerPackId}```");
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        SharePackChoiceResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.NoWait);
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        await bot.SendTextMessageAsync(
                result.ChatId,
                "Выберите опцию ниже:",
                replyMarkup: markup);
    }
}