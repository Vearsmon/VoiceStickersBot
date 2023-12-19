using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.ShowAll;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandHandlersObsolete.CommandHandlers;

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
        //В команду лучше добавить список всех единиц (паков или стикеров),
        //чтото обобщенное чтобы можно было использовать один метод для паков и стикеров
        var switchCommand = new SwitchKeyboardCommand(command.RequestContext, 0, "pageright:1",
            10, "Вот все ваши стикеры:");
        var switchHandler = new SwitchKeyboardHandler(switchCommand);
        
        return new ShowAllResult(command.RequestContext.UserBotState, (SwitchKeyboardResult)switchHandler.Handle());
    }
}