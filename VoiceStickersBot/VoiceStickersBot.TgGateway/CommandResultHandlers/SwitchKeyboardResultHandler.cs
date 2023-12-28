/*using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class SwitchKeyboardResultHandler : CommandResultHandlerBase<SwitchKeyboardResultObsoleteObsolete>
{
    public override Type ResultType => typeof(SwitchKeyboardResultObsoleteObsolete);
    public override async Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, SwitchKeyboardResultObsoleteObsolete commandResultObsoleteObsolete, CallbackQuery callback)
    {
        await bot.EditMessageReplyMarkupAsync(
            chatId: callback.From.Id,
            messageId: callback.Message!.MessageId,
            replyMarkup: GetMarkup(commandResultObsoleteObsolete)
        );
        return commandResultObsoleteObsolete.UserBotStateFrom;
    }

    public override async Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, SwitchKeyboardResultObsoleteObsolete commandResultObsoleteObsolete, Message message)
    {
        await bot.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: commandResultObsoleteObsolete.KeyboardCapture,
            replyMarkup: GetMarkup(commandResultObsoleteObsolete)
        );
        return commandResultObsoleteObsolete.UserBotStateFrom;
    }

    private InlineKeyboardMarkup GetMarkup(SwitchKeyboardResultObsoleteObsolete switchResultObsoleteObsolete)
    {
        var currentPageKeyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in switchResultObsoleteObsolete.InlineKeyboardDto.Buttons)
            currentPageKeyboard.Add(new[]
                { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in switchResultObsoleteObsolete.InlineKeyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        currentPageKeyboard.Add(lastRow.ToArray());
        
        return new InlineKeyboardMarkup(currentPageKeyboard.ToArray());
    }
}*/