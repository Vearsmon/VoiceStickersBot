using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class TgApiCommandHandlerService
{
    private readonly Dictionary<Type, ICommandHandlerFactory> commandHandlers;

    public TgApiCommandHandlerService(List<ICommandHandlerFactory> commandHandlers)
    {
        this.commandHandlers = commandHandlers.ToDictionary(
            key => key.CommandType,
            value => value);
    }

    public ICommandResult Handle(ICommand command)
    {
        var commandHandler = commandHandlers[command.GetType()].CreateCommandHandler(command);
        return commandHandler.Handle();
    }
}