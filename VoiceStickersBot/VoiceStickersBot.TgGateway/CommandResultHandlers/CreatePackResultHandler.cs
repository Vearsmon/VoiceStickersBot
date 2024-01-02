using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Core.CommandResults.CreatePackResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class CreatePackResultHandler : ICommandResultHandler
{
    public async Task Handle(ITelegramBotClient bot, CreatePackAddPackResult result)
    {
        //надо проверку на ошибки или отмену
        await bot.SendTextMessageAsync(result.ChatId, "Стикерпак успешно создан");
    }

    public async Task Handle(ITelegramBotClient bot, CreatePackSendInstructionsResult result)
    {
        await bot.SendTextMessageAsync(result.ChatId, "Отправьте мне название стикерпака");
    }
}