using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public interface ICommandResultHandler
{
    public Type ResultType { get; }
    public Task HandleWithCallback(ITelegramBotClient bot, ICommandResult commandResult, CallbackQuery callback);
    public Task HandleWithMessage(ITelegramBotClient bot, ICommandResult commandResult, Message message);
}