using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.AddSticker;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class AddStickerResultHandler : CommandResultHandlerBase<AddStickerResultObsolete>
{
    public override Type ResultType => typeof(AddStickerResultObsolete);
    public override Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, AddStickerResultObsolete commandResultObsolete, CallbackQuery callbackQuery)
    {
        throw new NotSupportedException();
    }

    public override async Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, AddStickerResultObsolete commandResultObsolete, Message message)
    {
        var switchKeyboardResult = commandResultObsolete.SwitchKeyboardResultObsolete;
        
        var handler = new SwitchKeyboardResultHandler();
        await handler.HandleFromMessage(bot, switchKeyboardResult, message);
        return UserBotState.WaitChoosePack;
    }
}