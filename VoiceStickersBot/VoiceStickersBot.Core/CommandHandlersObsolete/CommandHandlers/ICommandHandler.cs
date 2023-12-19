using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlersObsolete.CommandHandlers;

public interface ICommandHandler
{
    Type CommandType { get; }

    ICommandResult Handle();
}