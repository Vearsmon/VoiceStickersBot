using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public interface ICommandHandlerFactory
{
    Type CommandType { get; }

    public ICommandHandler CreateCommandHandler(ICommand command);
}