using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public abstract class CommandResultHandlerBase<TCommandResult> : ICommandResultHandler
    where TCommandResult : class, ICommandResultObsolete
{
    public abstract Type ResultType { get; }
    public Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, ICommandResultObsolete commandResultObsolete, Message message)
    {
        if (commandResultObsolete is not TCommandResult typedCommand)
            throw new InvalidOperationException(
                $"Invalid command type [{commandResultObsolete.GetType()}] for [{ResultType}] command handler");

        return HandleFromMessage(bot, typedCommand, message);
    }

    public Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, ICommandResultObsolete commandResultObsolete, CallbackQuery callback)
    {
        if (commandResultObsolete is not TCommandResult typedCommand)
            throw new InvalidOperationException(
                $"Invalid command type [{commandResultObsolete.GetType()}] for [{ResultType}] command handler");

        return HandleFromCallback(bot, typedCommand, callback);
    }

    public abstract Task<UserBotState> HandleFromCallback(ITelegramBotClient bot, TCommandResult commandResult, CallbackQuery callbackQuery);
    public abstract Task<UserBotState> HandleFromMessage(ITelegramBotClient bot, TCommandResult commandResult, Message message);
}