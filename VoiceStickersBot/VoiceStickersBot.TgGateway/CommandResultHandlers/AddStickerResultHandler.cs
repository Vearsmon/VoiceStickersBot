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

    private readonly Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>> handlers;

    public AddStickerResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>>
        {
            {
                typeof(AddStickerAddStickerResult),
                async (bot, res) => await Handle(bot, (AddStickerAddStickerResult)res)
            },
            {
                typeof(AddStickerSendStickerResult),
                (bot, res) => Handle(bot, (AddStickerSendStickerResult)res)
            },
            {
                typeof(AddStickerSwitchKeyboardPacksResult),
                (bot, res) => Handle(bot, (AddStickerSwitchKeyboardPacksResult)res)
            },
            {
                typeof(AddStickerSwitchKeyboardStickersResult),
                (bot, res) => Handle(bot, (AddStickerSwitchKeyboardStickersResult)res)
            },
            {
                typeof(AddStickerSendInstructionsResult),
                (bot, res) => Handle(bot, (AddStickerSendInstructionsResult)res)
            }
        };
    }

    public Task HandleResult(ITelegramBotClient bot, ICommandResult result)
    {
        return handlers[result.GetType()](bot, result);
    }

    private async Task Handle(ITelegramBotClient bot, AddStickerAddStickerResult result)
    {
        using var stream = new MemoryStream();
        await bot.GetInfoAndDownloadFileAsync(
            result.FileId,
            stream);

        await objectStorage.PutObjectInStorage(
            "objstorbucket",
            result.StickerId,
            MimeTypes.Mpeg,
            stream.ToArray());

        await bot.SendTextMessageAsync(
            result.ChatId,
            "Стикер успешно доавблен");
    }

    private async Task Handle(ITelegramBotClient bot, AddStickerSendStickerResult result)
    {
        var memoryStream = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var voiceFile = InputFile.FromStream(memoryStream);
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile);
    }

    private async Task Handle(ITelegramBotClient bot, AddStickerSendInstructionsResult result)
    {
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Выберите стикерпак, в который хотите добавить стикер, " +
            "а затем отправьте голосовое сообщение или аудиофайл с подписью - имя будущего стикера.");
    }

    private async Task Handle(ITelegramBotClient bot, AddStickerSwitchKeyboardPacksResult result)
    {
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        if (result.BotMessageId is null)
        {
            await bot.SendTextMessageAsync(
                result.ChatId,
                "Выберите набор в который хотите добавить стикер:",
                replyMarkup: markup);
        }
        else
        {
            await bot.EditMessageReplyMarkupAsync(
                inlineMessageId: result.BotMessageId,
                replyMarkup: markup);
        }
    }

    private async Task Handle(ITelegramBotClient bot, AddStickerSwitchKeyboardStickersResult result)
    {
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        if (result.BotMessageId is null)
        {
            var msg = await bot.SendTextMessageAsync(
                result.ChatId,
                "Вот все стикеры из выбранного набора. Отправьте голосовое или аудио файл с подписью," +
                "если хотите добавить в этот набор.",
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