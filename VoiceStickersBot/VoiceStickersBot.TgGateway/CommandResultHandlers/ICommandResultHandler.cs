using Telegram.Bot;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public interface ICommandResultHandler
{
    CommandType CommandType { get; }

    Task HandleResult(ITelegramBotClient bot, ICommandResult result);
}