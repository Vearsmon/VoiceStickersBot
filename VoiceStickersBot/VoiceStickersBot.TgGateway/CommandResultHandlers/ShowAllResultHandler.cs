using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;
using VoiceStickersBot.Infra.ObjectStorageCluster;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class ShowAllResultHandler
{
    private ObjectStorageClient objectStorage { get; }
    private ITelegramBotClient bot { get; }
    public ShowAllResultHandler(ITelegramBotClient bot)
    {
        this.bot = bot;
        objectStorage = new ObjectStorageClient();
    }
    
    public async Task HandleShowAllSendStickerResult(ShowAllSendStickerResult result)
    {
        var voiceBytes = await objectStorage.GetObjectFromStorage(ObjectLocation.Parse(result.Sticker.Location));
        var memorystream = new MemoryStream(voiceBytes);
        var voiceFile = InputFile.FromStream(memorystream);
        await bot.SendVoiceAsync(
            result.ChatId,
            voiceFile
            );
    }
    
    public async Task HandleShowAllSwitchKeyboardPacksResult(ShowAllSwitchKeyboardPacksResult result)
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

        await bot.EditMessageReplyMarkupAsync(
            inlineMessageId: result.BotMessageId,
            replyMarkup: markup
        );
    }
    
    public async Task HandleShowAllSwitchKeyboardStickersResult(ShowAllSwitchKeyboardStickersResult result)
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

        await bot.EditMessageReplyMarkupAsync(
            inlineMessageId: result.BotMessageId,
            replyMarkup: markup
        );
    }
}