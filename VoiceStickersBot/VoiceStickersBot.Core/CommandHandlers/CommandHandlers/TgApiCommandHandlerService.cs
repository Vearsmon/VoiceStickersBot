using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.CommandResults;

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

    public async Task<IHandleCommandResult> Handle(ICommandArguments commandArguments)
    {
        ICommandResult result = null;
        Exception error = null;
        
        var commandHandler = commandHandlersFactories[commandArguments.CommandType]
            .CreateCommandHandler(commandArguments);
        result = await commandHandler.Handle().ConfigureAwait(false);

        return new HandleCommandResult(result, error);
    }
}