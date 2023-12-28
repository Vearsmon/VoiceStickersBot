/*using VoiceStickersBot.Core.CommandHandlersObsolete.CommandHandlerFactory;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlersObsolete.CommandHandlers;

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
        ICommandResultObsoleteObsolete resultObsoleteObsolete = null;
        Exception error = null;
        try
        {
            var commandHandler = commandHandlersFactories[command.GetType()].CreateCommandHandler(command); 
            resultObsoleteObsolete = commandHandler.Handle();
        }
        catch (Exception ex)
        {
            error = ex;
            Console.WriteLine("oh boy :(");
        }

        return new HandleCommandResult(resultObsoleteObsolete, error);
    }
}*/