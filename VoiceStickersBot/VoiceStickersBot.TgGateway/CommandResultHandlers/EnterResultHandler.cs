using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands.EnterCommand;
using NotSupportedException = System.NotSupportedException;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class EnterResultHandler : CommandResultHandlerBase<EnterResult>
{
    public override Type ResultType => typeof(EnterResult);
    public override async Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, EnterResult commandResult, CallbackQuery callbackQuery)
    {
        
        /*await n
        return UserBotState.WaitChooseSticker;*/
        throw new NotImplementedException();
    }

    public override Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, EnterResult commandResult, Message message)
    {
        throw new NotSupportedException();
    }
}