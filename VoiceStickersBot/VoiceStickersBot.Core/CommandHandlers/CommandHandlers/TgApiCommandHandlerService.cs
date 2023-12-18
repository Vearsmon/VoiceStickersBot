using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class TgApiCommandHandlerService
{
    private readonly Dictionary<Type, ICommandHandlerFactory> commandHandlersFactories;

    public TgApiCommandHandlerService(List<ICommandHandlerFactory> commandHandlersFactories)
    {
        this.commandHandlersFactories = commandHandlersFactories.ToDictionary(
            key => key.CommandType,
            value => value);
    }

    public IHandleCommandResult Handle(ICommand command)
    {
        ICommandResult result = null;
        Exception error = null;
        try
        {
            var commandHandler = commandHandlersFactories[command.GetType()].CreateCommandHandler(command); 
            result = commandHandler.Handle();
        }
        catch (Exception ex)
        {
            error = ex;
            Console.WriteLine("oh boy :(");
        }

        return new HandleCommandResult(result, error);
    }
}