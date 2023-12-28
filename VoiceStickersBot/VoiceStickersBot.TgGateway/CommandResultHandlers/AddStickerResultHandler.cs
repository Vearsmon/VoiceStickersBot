/*using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;
using VoiceStickersBot.Core.CommandsObsolete.AddStickerObsolete;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class AddStickerResultHandler : CommandResultHandlerBase<AddStickerResultObsoleteObsolete>
{
    public override Type ResultType => typeof(AddStickerResultObsoleteObsolete);
    public override Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, AddStickerResultObsoleteObsolete commandResultObsoleteObsolete, CallbackQuery callbackQuery)
    {
        throw new NotSupportedException();
    }

    public override async Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, AddStickerResultObsoleteObsolete commandResultObsoleteObsolete, Message message)
    {
        var switchKeyboardResult = commandResultObsoleteObsolete.SwitchKeyboardResultObsoleteObsolete;
        
        var handler = new SwitchKeyboardResultHandler();
        await handler.HandleFromMessage(bot, switchKeyboardResult, message);
        return UserBotState.WaitChoosePack;
    }
}*/