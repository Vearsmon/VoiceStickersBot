using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class SwitchKeyboardResultHandler : CommandResultHandlerBase<SwitchKeyboardResultObsolete>
{
    public override Type ResultType => typeof(SwitchKeyboardResultObsolete);
    public override async Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, SwitchKeyboardResultObsolete commandResultObsolete, CallbackQuery callback)
    {
        await bot.EditMessageReplyMarkupAsync(
            chatId: callback.From.Id,
            messageId: callback.Message!.MessageId,
            replyMarkup: GetMarkup(commandResultObsolete)
        );
        return commandResultObsolete.UserBotStateFrom;
    }

    public override async Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, SwitchKeyboardResultObsolete commandResultObsolete, Message message)
    {
        await bot.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: commandResultObsolete.KeyboardCapture,
            replyMarkup: GetMarkup(commandResultObsolete)
        );
        return commandResultObsolete.UserBotStateFrom;
    }

    private InlineKeyboardMarkup GetMarkup(SwitchKeyboardResultObsolete switchResultObsolete)
    {
        var currentPageKeyboard = new List<InlineKeyboardButton[]>();
        foreach (var button in switchResultObsolete.InlineKeyboardDto.Buttons)
            currentPageKeyboard.Add(new[]
                { InlineKeyboardButton.WithCallbackData(button.ButtonText, button.CallbackData) });
        var lastRow = new List<InlineKeyboardButton>();
        foreach (var lastButton in switchResultObsolete.InlineKeyboardDto.LastButtons)
            lastRow.Add(InlineKeyboardButton.WithCallbackData(lastButton.ButtonText, lastButton.CallbackData));
        currentPageKeyboard.Add(lastRow.ToArray());
        
        return new InlineKeyboardMarkup(currentPageKeyboard.ToArray());
    }
}