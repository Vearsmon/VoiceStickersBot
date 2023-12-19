using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public interface ICommandHandler
{
    CommandType CommandType { get; }

    Task<ICommandResult> Handle();
}