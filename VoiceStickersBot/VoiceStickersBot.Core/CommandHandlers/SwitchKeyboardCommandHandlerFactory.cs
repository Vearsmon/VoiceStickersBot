using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandHandlers;

public class SwitchKeyboardCommandHandlerFactory : CommandHandlerFactoryBase<SwitchKeyboardCommand>
{
    public override Type CommandType => typeof(SwitchKeyboardCommand);

    protected override ICommandHandler CreateCommandHandler(SwitchKeyboardCommand command)
    {
        return new SwitchKeyboardHandler(command);
    }
}