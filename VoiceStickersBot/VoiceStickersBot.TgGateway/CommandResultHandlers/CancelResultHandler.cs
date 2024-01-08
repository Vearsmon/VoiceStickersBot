using Telegram.Bot;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.CancelResult;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class CancelResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.Cancel;
    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public CancelResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>>()
        {
            {
                typeof(CancelCancelResult),
                async (bot, infos, res) => await Handle(bot, infos, (CancelCancelResult)res)
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

    private async Task Handle(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        CancelCancelResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);

        await bot.SendTextMessageAsync(
            result.ChatId,
            "Команда отменена",
            replyMarkup: DefaultKeyboard.CommandsKeyboard);
    }
}