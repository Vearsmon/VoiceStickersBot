using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public abstract class CommandResultHandlerBase<TCommandResult> : ICommandResultHandler
    where TCommandResult : class, ICommandResult
{
    public abstract Type ResultType { get; }
    public Task HandleWithMessage(ITelegramBotClient bot, ICommandResult commandResult, Message message)
    {
        if (commandResult is not TCommandResult typedCommand)
            throw new InvalidOperationException(
                $"Invalid command type [{commandResult.GetType()}] for [{ResultType}] command handler");

        return HandleWithMessage(bot, typedCommand, message);
    }

    public Task HandleWithCallback(ITelegramBotClient bot, ICommandResult commandResult, CallbackQuery callback)
    {
        if (commandResult is not TCommandResult typedCommand)
            throw new InvalidOperationException(
                $"Invalid command type [{commandResult.GetType()}] for [{ResultType}] command handler");

        return HandleWithCallback(bot, typedCommand, callback);
    }

    public abstract Task HandleWithCallback(ITelegramBotClient bot, TCommandResult commandResult, CallbackQuery callbackQuery);
    public abstract Task HandleWithMessage(ITelegramBotClient bot, TCommandResult commandResult, Message message);
}