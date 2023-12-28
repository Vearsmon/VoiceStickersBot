/*using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class TgApiCommandResultHandlerServiceObsolete
{
    private readonly Dictionary<Type, ICommandResultHandler> commandResultHandlers;

    public TgApiCommandResultHandlerServiceObsolete(List<ICommandResultHandler> commandResultHandlers)
    {
        this.commandResultHandlers = commandResultHandlers.ToDictionary(
            key => key.ResultType,
            value => value);
    }

    public async Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, ICommandResultObsoleteObsolete commandResultObsoleteObsolete, CallbackQuery callback)
    {
        var resultHandler = commandResultHandlers[commandResultObsoleteObsolete.GetType()];
        return await resultHandler.HandleResult(bot, commandResultObsoleteObsolete, callback);
    }
    
    public async Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, ICommandResultObsoleteObsolete commandResultObsoleteObsolete, Message message)
    {
        var resultHandler = commandResultHandlers[commandResultObsoleteObsolete.GetType()]; 
        return await resultHandler.HandleFromMessage(bot, commandResultObsoleteObsolete, message);
    }
}*/