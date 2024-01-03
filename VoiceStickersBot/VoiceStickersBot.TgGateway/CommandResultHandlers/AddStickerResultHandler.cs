using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class AddStickerResultHandler : ICommandResultHandler
{
    private readonly ObjectStorageClient objectStorage = new();

    public async Task Handle(ITelegramBotClient bot, AddStickerAddStickerResult result)
    {
        using var stream = new MemoryStream();
        await bot.GetInfoAndDownloadFileAsync(
            result.FileId,
            stream);
        
        await objectStorage.PutObjectInStorage("location",
            Guid.NewGuid(),
            "Audio/mpeg",
            stream.ToArray()
        );

        await bot.SendTextMessageAsync(
            result.ChatId,
            "Стикер успешно доавблен");
    }
    
    public async Task Handle(ITelegramBotClient bot, AddStickerSendStickerResult result)
    {
        var voiceBytes = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var memoryStream = new MemoryStream(voiceBytes);
        var voiceFile = InputFile.FromStream(memoryStream);
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile);
    }
    
    public async Task Handle(ITelegramBotClient bot, AddStickerSendInstructionsResult result)
    {
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Выберите стикерпак, в который хотите добавить стикер, " +
            "а затем отправьте голосовое сообщение или аудиофайл с подписью - имя будущего стикера.");
    }
    
    public async Task Handle(ITelegramBotClient bot, AddStickerSwitchKeyboardPacksResult result)
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
    
    public async Task Handle(ITelegramBotClient bot, AddStickerSwitchKeyboardStickersResult result)
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