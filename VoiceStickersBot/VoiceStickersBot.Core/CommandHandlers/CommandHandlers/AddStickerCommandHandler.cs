using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.AddSticker;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class AddStickerCommandHandler : ICommandHandler
{
    public Type CommandType => typeof(AddStickerCommand);
    private readonly ICommand command;

    public AddStickerCommandHandler(ICommand command)
    {
        this.command = command;
    }
    
    public ICommandResult Handle()
    {
        //В команду лучше добавить список всех единиц (паков или стикеров),
        //чтото обобщенное чтобы можно было использовать один метод для паков и стикеров
        var switchCommand = new SwitchKeyboardCommand(0, "pageright:1",
            10, "Выберите набор, в который хотите добавить стикер:");
        var switchHandler = new SwitchKeyboardHandler(switchCommand);
        
        return switchHandler.Handle();
    }
}