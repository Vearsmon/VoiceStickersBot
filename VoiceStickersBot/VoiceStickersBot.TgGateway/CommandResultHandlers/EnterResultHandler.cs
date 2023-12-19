using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands.EnterCommand;
using NotSupportedException = System.NotSupportedException;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class EnterResultHandler : CommandResultHandlerBase<EnterResultObsolete>
{
    public override Type ResultType => typeof(EnterResultObsolete);
    public override async Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, EnterResultObsolete commandResultObsolete, CallbackQuery callbackQuery)
    {
        
        /*await n
        return UserBotState.WaitChooseSticker;*/
        throw new NotImplementedException();
    }

    public override Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, EnterResultObsolete commandResultObsolete, Message message)
    {
        throw new NotSupportedException();
    }
}