using Telegram.Bot;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.CreatePackResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class CreatePackResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.CreatePack;

    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public CreatePackResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>,ICommandResult, Task>>
        {
            {
                typeof(CreatePackAddPackResult),
                (bot, infos, res) => Handle(bot, infos, (CreatePackAddPackResult)res)
            },
            {
                typeof(CreatePackSendInstructionsResult),
                (bot, infos, res) => Handle(bot, infos, (CreatePackSendInstructionsResult)res)
            }
        };
    }

    public Task HandleResult(ITelegramBotClient bot, Dictionary<long, UserInfo> userInfos, ICommandResult result)
    {
        return handlers[result.GetType()](bot, userInfos, result);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        CreatePackAddPackResult result)
    {
        //надо проверку на ошибки или отмену
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);
        
        await bot.SendTextMessageAsync(result.ChatId, "Стикерпак успешно создан",
            replyMarkup: DefaultKeyboard.CommandsKeyboard);
    }

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        CreatePackSendInstructionsResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.WaitPackName);
        
        await bot.SendTextMessageAsync(result.ChatId, "Отправьте мне название стикерпака");
    }
}