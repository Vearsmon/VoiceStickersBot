using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public interface ICommandHandler
{
    Type CommandType { get; }

    IHandleCommandResult Handle();
}