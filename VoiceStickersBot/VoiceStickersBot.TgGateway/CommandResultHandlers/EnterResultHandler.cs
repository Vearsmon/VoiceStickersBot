/*using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands.EnterCommand;
using NotSupportedException = System.NotSupportedException;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class EnterResultHandler : CommandResultHandlerBase<EnterResultObsoleteObsolete>
{
    public override Type ResultType => typeof(EnterResultObsoleteObsolete);
    public override async Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, EnterResultObsoleteObsolete commandResultObsoleteObsolete, CallbackQuery callbackQuery)
    {
        
        /*await n
        return UserBotState.WaitChooseSticker;#1#
        throw new NotImplementedException();
    }

    public override Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, EnterResultObsoleteObsolete commandResultObsoleteObsolete, Message message)
    {
        throw new NotSupportedException();
    }
}*/