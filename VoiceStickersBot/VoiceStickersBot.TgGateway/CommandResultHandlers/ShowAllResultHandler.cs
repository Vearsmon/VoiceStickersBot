using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.ShowAll;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class ShowAllResultHandler : CommandResultHandlerBase<ShowAllResult>
{
    public override Type ResultType => typeof(ShowAllResult);
    public override Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, ShowAllResult commandResult, CallbackQuery callbackQuery)
    {
        throw new NotSupportedException();
    }

    public override async Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, ShowAllResult commandResult, Message message)
    {
        var switchKeyboardResult = commandResult.SwitchKeyboardResult;
        
        var handler = new SwitchKeyboardResultHandler();
        await handler.HandleFromMessage(bot, switchKeyboardResult, message);
        return UserBotState.WaitChoosePack;
    }
}