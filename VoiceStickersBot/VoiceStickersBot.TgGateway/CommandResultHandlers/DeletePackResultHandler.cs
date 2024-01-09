using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeletePackResults;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class DeletePackResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.DeletePack;

    private readonly ObjectStorageClient objectStorage = new();
    
    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public DeletePackResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>>()
        {
            {
                typeof(DeletePackSwitchKeyboardPacksResult),
                async (bot, infos, res) => await Handle(bot, infos, (DeletePackSwitchKeyboardPacksResult)res)
            },
            {
                typeof(DeletePackSwitchKeyboardStickersResult),
                async (bot, infos, res) => await Handle(bot, infos, (DeletePackSwitchKeyboardStickersResult)res)
            },
            {
                typeof(DeletePackSendStickerResult),
                async (bot, infos, res) => await Handle(bot, infos, (DeletePackSendStickerResult)res)
            },
            {
                typeof(DeletePackDeletePackResult),
                async (bot, infos, res) => await Handle(bot, infos, (DeletePackDeletePackResult)res)
            },
            {
                typeof(DeletePackConfirmResult),
                async (bot, infos, res) => await Handle(bot, infos, (DeletePackConfirmResult)res)
            }
        };
    }

    public Task HandleResult(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        ICommandResult result)
    {
        return handlers[result.GetType()](bot, userInfos, result);
    }
    
    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        DeletePackDeletePackResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);

        
        if (!Enum.TryParse(result.ChatType, out ChatType chatType))
            throw new ArgumentException($"Wrong ChatType: {result.ChatType}");

        var keyboard = chatType == ChatType.Private ? Keyboards.DialogKeyboard : Keyboards.GroupKeyboard;
        
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Стикерпак успешно удалён",
            replyMarkup: keyboard);
    }
    
    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        DeletePackSwitchKeyboardPacksResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);

        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        var message = result.HasPacks 
            ? "Выберите стикерпак, который хотите удалить:"
            : "У вас нет стикерпаков";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        DeletePackSwitchKeyboardStickersResult result)
    {
        userInfos[result.ChatId] = new UserInfo(
            UserState.WaitStickerChoice,
            stickerPackId: result.StickerPackId.ToString());
        
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        var message = "Вот все стикеры из выбранного стикерпака:";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        DeletePackSendStickerResult result)
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
        DeletePackConfirmResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);

        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);
        
        var message = "Вы точно хотите удалить этот стикерпак?";
        var botMessageId = result.BotMessageId;
        await BotSendExtensions.SendOrEdit(bot, botMessageId, message, markup, result.ChatId);
    }
}