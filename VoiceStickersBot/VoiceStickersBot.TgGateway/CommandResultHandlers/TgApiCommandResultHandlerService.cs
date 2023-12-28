using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class TgApiCommandResultHandlerService
{
    private readonly Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>> commandResultHandlers;
    private readonly ShowAllResultHandler sharh = new ();
    public TgApiCommandResultHandlerService(List<ICommandResultHandler> commandResultHandlers)
    {
        this.commandResultHandlers = new Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>>()
        {
            { 
                typeof(ShowAllSendStickerResult), 
                async (bot, res) => await sharh.Handle(bot, (ShowAllSendStickerResult)res) 
            },
            {
                typeof(ShowAllSwitchKeyboardStickersResult), 
                async (bot, res) => await sharh.Handle(bot, (ShowAllSwitchKeyboardStickersResult)res)
            },
            {
                typeof(ShowAllSwitchKeyboardPacksResult), 
                async (bot, res) => await sharh.Handle(bot, (ShowAllSwitchKeyboardPacksResult)res) 
            }
        };
    }

    public async Task HandleResult(ITelegramBotClient bot, ICommandResult commandResult)
    {
        var handlerFunc = commandResultHandlers[commandResult.GetType()];
        await handlerFunc(bot, commandResult);
    }
}