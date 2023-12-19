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

    public async Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, ICommandResultObsolete commandResultObsolete, CallbackQuery callback)
    {
        var resultHandler = commandResultHandlers[commandResultObsolete.GetType()];
        return await resultHandler.HandleFromCallback(bot, commandResultObsolete, callback);
    }
    
    public async Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, ICommandResultObsolete commandResultObsolete, Message message)
    {
        var resultHandler = commandResultHandlers[commandResultObsolete.GetType()]; 
        return await resultHandler.HandleFromMessage(bot, commandResultObsolete, message);
    }
}