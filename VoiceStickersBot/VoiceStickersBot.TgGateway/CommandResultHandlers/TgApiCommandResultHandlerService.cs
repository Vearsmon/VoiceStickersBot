using Telegram.Bot;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class TgApiCommandResultHandlerService
{
    private readonly Dictionary<CommandType, ICommandResultHandler> commandResultHandlers;

    public TgApiCommandResultHandlerService(List<ICommandResultHandler> commandResultHandlers)
    {
        this.commandResultHandlers = commandResultHandlers.ToDictionary(
            h => h.CommandType,
            h => h);
    }

    public Task HandleResult(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        ICommandResult commandResult)
    {
        //TODO: пусть принимает ссылку на словарь стейтов и передает в хендлрезалт
        var handler = commandResultHandlers[commandResult.CommandType];
        return handler.HandleResult(bot, userInfos, commandResult);
    }
}