using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public interface ICommandHandler
{
    CommandType CommandType { get; }

    ICommandResult Handle();
}