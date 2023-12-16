using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class SwitchKeyboardResultHandler : CommandResultHandlerBase<SwitchKeyboardResult>
{
    public override Type ResultType => typeof(SwitchKeyboardResult);
    public override async Task HandleWithCallback(ITelegramBotClient bot, SwitchKeyboardResult commandResult, CallbackQuery callback)
    {
        await bot.EditMessageReplyMarkupAsync(
            chatId: callback.From.Id,
            messageId: callback.Message!.MessageId,
            replyMarkup: GetMarkup(commandResult)
        );
    }

    public override async Task HandleWithMessage(ITelegramBotClient bot, SwitchKeyboardResult commandResult, Message message)
    {
        await bot.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: commandResult.KeyboardCapture,
            replyMarkup: GetMarkup(commandResult)
        );
    }

    private InlineKeyboardMarkup GetMarkup(SwitchKeyboardResult switchResult)
    {
        var currentPageKeyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in switchResult.InlineKeyboardDto.Buttons)
            currentPageKeyboard.Add(new[]
                { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in switchResult.InlineKeyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        currentPageKeyboard.Add(lastRow.ToArray());
        return new InlineKeyboardMarkup(currentPageKeyboard.ToArray());
    }
}