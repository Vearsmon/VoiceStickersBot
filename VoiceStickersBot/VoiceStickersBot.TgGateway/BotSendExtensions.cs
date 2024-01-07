using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace VoiceStickersBot.TgGateway;

public static class BotSendExtensions
{
    public static async Task SendOrEdit(
        ITelegramBotClient bot,
        int? botMessageId,
        string message,
        IReplyMarkup markup,
        long chatId)
    {
        if (botMessageId is null)
        {
            await bot.SendTextMessageAsync(
                chatId,
                message,
                replyMarkup: markup);
        }
        else
        {
            await bot.EditMessageTextAsync(
                chatId: chatId,
                messageId: botMessageId.Value,
                text: message);
            
            await bot.EditMessageReplyMarkupAsync(
                chatId: chatId,
                messageId: botMessageId.Value,
                replyMarkup: (InlineKeyboardMarkup)markup);
        }
    }
}