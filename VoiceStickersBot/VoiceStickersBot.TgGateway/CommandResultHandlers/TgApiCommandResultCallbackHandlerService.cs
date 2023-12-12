using Telegram.Bot;
using Telegram.Bot.Types;
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

    public async Task HandleWithCallback(ITelegramBotClient bot, ICommandResult commandResult, CallbackQuery callback)
    {
        var resultHandler = commandResultHandlers[commandResult.GetType()];
        await resultHandler.HandleWithCallback(bot, commandResult, callback);
    }
    
    public async Task HandleWithMessage(ITelegramBotClient bot, ICommandResult commandResult, Message message)
    {
        var resultHandler = commandResultHandlers[commandResult.GetType()]; 
        await resultHandler.HandleWithMessage(bot, commandResult, message);
    }
}