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

    private readonly Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>> handlers;

    public ShowAllResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>>
        {
            {
                typeof(ShowAllSendStickerResult),
                (bot, res) => Handle(bot, (ShowAllSendStickerResult)res)
            },
            {
                typeof(ShowAllSwitchKeyboardStickersResult),
                (bot, res) => Handle(bot, (ShowAllSwitchKeyboardStickersResult)res)
            },
            {
                typeof(ShowAllSwitchKeyboardPacksResult),
                (bot, res) => Handle(bot, (ShowAllSwitchKeyboardPacksResult)res)
            }
        };
    }

    public Task HandleResult(ITelegramBotClient bot, ICommandResult result)
    {
        return handlers[result.GetType()](bot, result);
    }

    private async Task Handle(ITelegramBotClient bot, ShowAllSendStickerResult result)
    {
        var memoryStream = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var voiceFile = InputFile.FromStream(memoryStream);
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile);
    }

    private async Task Handle(ITelegramBotClient bot, ShowAllSwitchKeyboardPacksResult result)
    {
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

    private async Task Handle(ITelegramBotClient bot, ShowAllSwitchKeyboardStickersResult result)
    {
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