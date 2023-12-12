using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class ShowAllResultHandler : ICommandResultHandler
{
    public Type ResultType => typeof(ShowAllResult);
    public Task HandleWithCallback(ITelegramBotClient bot, ICommandResult commandResult, CallbackQuery callback)
    {
        throw new NotImplementedException();
    }

    public Task HandleWithMessage(ITelegramBotClient bot, ICommandResult commandResult, Message message)
    {
        throw new NotImplementedException();
    }
}