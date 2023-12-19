using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.ShowAll;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class ShowAllSwitchKeyboardPacksCommandHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.ShowAll;

    private readonly ShowAllCancelCommandArguments commandArguments;
    
    public ShowAllSwitchKeyboardPacksCommandHandler(ShowAllCancelCommandArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }
    
    public ICommandResult Handle()
    {
        //В команду лучше добавить список всех единиц (паков или стикеров),
        //чтото обобщенное чтобы можно было использовать один метод для паков и стикеров
        var switchCommand = new SwitchKeyboardCommand(commandArguments.RequestContext, 0, "pageright:1",
            10, "Вот все ваши стикеры:");
        var switchHandler = new SwitchKeyboardHandler(switchCommand);
        
        return new ShowAllResult(commandArguments.RequestContext.UserBotState, (SwitchKeyboardResult)switchHandler.Handle());
    }
}