using Telegram.Bot;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.CreatePackResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class CreatePackResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.CreatePack;

    private readonly Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>> handlers;

    public CreatePackResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>>
        {
            {
                typeof(CreatePackAddPackResult),
                (bot, res) => Handle(bot, (CreatePackAddPackResult)res)
            },
            {
                typeof(CreatePackSendInstructionsResult),
                (bot, res) => Handle(bot, (CreatePackSendInstructionsResult)res)
            }
        };
    }

    public Task HandleResult(ITelegramBotClient bot, ICommandResult result)
    {
        return handlers[result.GetType()](bot, result);
    }

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