using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class TgApiCommandResultCallbackHandlerService
{
    private readonly Dictionary<Type, ICommandResultHandler> commandResultHandlers;

    public TgApiCommandResultCallbackHandlerService(List<ICommandResultHandler> commandResultHandlers)
    {
        this.commandResultHandlers = commandResultHandlers.ToDictionary(
            key => key.ResultType,
            value => value);
    }

    public async Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, ICommandResult commandResult, CallbackQuery callback)
    {
        var resultHandler = commandResultHandlers[commandResult.GetType()];
        return await resultHandler.HandleFromCallback(bot, commandResult, callback);
    }
    
    public async Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, ICommandResult commandResult, Message message)
    {
        var resultHandler = commandResultHandlers[commandResult.GetType()]; 
        return await resultHandler.HandleFromMessage(bot, commandResult, message);
    }
}