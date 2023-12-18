using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.AddSticker;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class AddStickerResultHandler : CommandResultHandlerBase<AddStickerResult>
{
    public override Type ResultType => typeof(AddStickerResult);
    public override Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, AddStickerResult commandResult, CallbackQuery callbackQuery)
    {
        throw new NotSupportedException();
    }

    public override async Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, AddStickerResult commandResult, Message message)
    {
        var switchKeyboardResult = commandResult.SwitchKeyboardResult;
        
        var handler = new SwitchKeyboardResultHandler();
        await handler.HandleFromMessage(bot, switchKeyboardResult, message);
        return UserBotState.WaitChoosePack;
    }
}