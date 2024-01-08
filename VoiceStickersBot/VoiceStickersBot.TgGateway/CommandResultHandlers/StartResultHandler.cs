using Telegram.Bot;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.StartResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class StartResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.Start;
    
    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public StartResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>>()
        {
            {
                typeof(StartStartResult),
                async (bot, infos, res) => await Handle(bot, infos, (StartStartResult)res)
            }
        };
    }
    
    public Task HandleResult(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        ICommandResult result)
    {
        return handlers[result.GetType()](bot, userInfos, result);
    }

    private async Task Handle(ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        StartStartResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);

        await bot.SendTextMessageAsync(
            result.ChatId,
            "Для взаимодействия с ботом используйте кнопки ниже.\n" +
            "Для начала создайте или импортируйте стикерпак",
            replyMarkup: DefaultKeyboard.CommandsKeyboard);
    }
}