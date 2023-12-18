using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public interface ICommandResultHandler
{
    public Type ResultType { get; }
    public Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, ICommandResult commandResult, CallbackQuery callback);
    public Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, ICommandResult commandResult, Message message);
}