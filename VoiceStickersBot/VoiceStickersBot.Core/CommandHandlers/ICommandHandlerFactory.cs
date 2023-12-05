using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlers;

public interface ICommandHandlerFactory
{
    Type CommandType { get; }

    public ICommandHandler CreateCommandHandler(ICommand command);
}