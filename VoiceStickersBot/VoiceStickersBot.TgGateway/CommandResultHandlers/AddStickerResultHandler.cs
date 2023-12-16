using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.AddSticker;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class AddStickerResultHandler : CommandResultHandlerBase<AddStickerResult>
{
    public override Type ResultType => typeof(AddStickerResult);
    public override Task HandleWithCallback(ITelegramBotClient bot, AddStickerResult commandResult, CallbackQuery callbackQuery)
    {
        throw new NotSupportedException();
    }

    public override async Task HandleWithMessage(ITelegramBotClient bot, AddStickerResult commandResult, Message message)
    {
        var abstractCommandResult = (ICommandResult)commandResult;
        var switchKeyboardResult = (SwitchKeyboardResult)abstractCommandResult;
        
        var handler = new SwitchKeyboardResultHandler();
        await handler.HandleWithMessage(bot, switchKeyboardResult, message);
    }
}