using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;
using VoiceStickersBot.Core.CommandResults.CreatePackResults;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class TgApiCommandResultHandlerService
{
    private readonly Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>> commandResultHandlers;
    private readonly ShowAllResultHandler showAllResultHandler = new();
    private readonly CreatePackResultHandler createPackResultHandler = new();
    private readonly AddStickerResultHandler addStickerResultHandler = new();
    
    public TgApiCommandResultHandlerService(List<ICommandResultHandler> commandResultHandlers)
    {
        this.commandResultHandlers = new Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>>()
        {
            { 
                typeof(ShowAllSendStickerResult), 
                async (bot, res) => await showAllResultHandler.Handle(bot, (ShowAllSendStickerResult)res) 
            },
            {
                typeof(ShowAllSwitchKeyboardStickersResult), 
                async (bot, res) => await showAllResultHandler.Handle(bot, (ShowAllSwitchKeyboardStickersResult)res)
            },
            {
                typeof(ShowAllSwitchKeyboardPacksResult), 
                async (bot, res) => await showAllResultHandler.Handle(bot, (ShowAllSwitchKeyboardPacksResult)res) 
            }, 
            {
                typeof(CreatePackAddPackResult), 
                async (bot, res) => await createPackResultHandler.Handle(bot, (CreatePackAddPackResult)res) 
            },
            {
                typeof(CreatePackSendInstructionsResult), 
                async (bot, res) => await createPackResultHandler.Handle(bot, (CreatePackSendInstructionsResult)res) 
            },
            {
                typeof(AddStickerAddStickerResult),
                async (bot, res) => await addStickerResultHandler.Handle(bot, (AddStickerAddStickerResult)res)
            },
            {
                typeof(AddStickerSendStickerResult),
                async (bot, res) => await addStickerResultHandler.Handle(bot, (AddStickerSendStickerResult)res)
            },
            {
                typeof(AddStickerSwitchKeyboardPacksResult),
                async (bot, res) => await addStickerResultHandler.Handle(bot, (AddStickerSwitchKeyboardPacksResult)res)
            },
            {
                typeof(AddStickerSwitchKeyboardStickersResult),
                async (bot, res) => await addStickerResultHandler.Handle(bot, (AddStickerSwitchKeyboardStickersResult)res)
            },
            {
                typeof(AddStickerSendInstructionsResult),
                async (bot, res) => await addStickerResultHandler.Handle(bot, (AddStickerSendInstructionsResult)res)
            }
        };
    }

    public async Task HandleResult(ITelegramBotClient bot, ICommandResult commandResult)
    {
        var handlerFunc = commandResultHandlers[commandResult.GetType()];
        await handlerFunc(bot, commandResult);
    }
}