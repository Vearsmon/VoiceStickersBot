using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class TgApiCommandHandlerService
{
    private readonly Dictionary<CommandType, ICommandHandlerFactory> commandHandlersFactories;

    public TgApiCommandHandlerService(List<ICommandHandlerFactory> commandHandlersFactories)
    {
        this.commandHandlersFactories = commandHandlersFactories.ToDictionary(
            key => key.CommandType,
            value => value);
    }

    public IHandleCommandResult Handle(ICommandArguments commandArguments)
    {
        ICommandResultObsolete resultObsolete = null;
        Exception error = null;
        try
        {
            var commandHandler = commandHandlersFactories[commandArguments.CommandType]
                .CreateCommandHandler(commandArguments);
            resultObsolete = commandHandler.Handle();
        }
        catch (Exception ex)
        {
            error = ex;
            Console.WriteLine("oh boy :(");
        }

        return new HandleCommandResult(resultObsolete, error);
    }
}