using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class ShowAllResultHandler : ICommandResultHandler
{
    private readonly ObjectStorageClient objectStorage = new();

    public async Task Handle(ITelegramBotClient bot, ShowAllSendStickerResult result)
    {
        var voiceBytes = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var memoryStream = new MemoryStream(voiceBytes);
        var voiceFile = InputFile.FromStream(memoryStream);
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile);
    }

    public async Task Handle(ITelegramBotClient bot, ShowAllSwitchKeyboardPacksResult result)
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

    public async Task Handle(ITelegramBotClient bot, ShowAllSwitchKeyboardStickersResult result)
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