using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;
using VoiceStickersBot.Infra.ObjectStorageCluster;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class ShowAllResultHandler : ICommandResultHandler
{
    private readonly ObjectStorageClient objectStorage = new();

    public async Task Handle(ITelegramBotClient bot, ShowAllSendStickerResult result)
    {
        var voiceBytes = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var memorystream = new MemoryStream(voiceBytes);
        var voiceFile = InputFile.FromStream(memorystream);
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile);
    }
    
    public async Task Handle(ITelegramBotClient bot, ShowAllSwitchKeyboardPacksResult result)
    {
        var currentPageKeyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in result.KeyboardDto.Buttons)
            currentPageKeyboard.Add(new[]
                { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in result.KeyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        currentPageKeyboard.Add(lastRow.ToArray());
        var markup =  new InlineKeyboardMarkup(currentPageKeyboard.ToArray());

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
        var currentPageKeyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in result.KeyboardDto.Buttons)
            currentPageKeyboard.Add(new[]
                { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in result.KeyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        currentPageKeyboard.Add(lastRow.ToArray());
        var markup =  new InlineKeyboardMarkup(currentPageKeyboard.ToArray());

        if (result.BotMessageId is null)
        {
            await bot.SendTextMessageAsync(
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