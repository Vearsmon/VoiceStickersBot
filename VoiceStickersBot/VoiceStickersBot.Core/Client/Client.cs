using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Client;

public class Client
{
    public Client()
    {
    }

    public TCommandResult Handle<TCommandResult>(ICommand command)
    {
        var handlerFactory = new SwitchKeyboardCommandHandlerFactory();
        var chl = new List<ICommandHandlerFactory> { handlerFactory };
        var mainHandler = new TgApiCommandHandlerService(chl);

        var result = mainHandler.Handle(command);
        if (result.EnsureSuccess)
            return (TCommandResult)result;
        
        throw result.Error;
    }
}