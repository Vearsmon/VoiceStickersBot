using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.ShowAll;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class ShowAllHandler : ICommandHandler
{
    public Type CommandType => typeof(ShowAllCommand);

    private readonly ShowAllCommand command;
    
    public ShowAllHandler(ShowAllCommand command)
    {
        this.command = command;
    }
    
    public ICommandResult Handle()
    {
        var switchCommand = new SwitchKeyboardCommand(0, "pageright:1", 10);
        var switchHandler = new SwitchKeyboardHandler(switchCommand);
        
        return switchHandler.Handle();
    }
}