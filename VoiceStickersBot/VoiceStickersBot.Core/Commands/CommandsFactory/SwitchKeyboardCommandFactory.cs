using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public class SwitchKeyboardCommandFactory : CommandFactoryBase
{
    public override IReadOnlyList<string> CommandPrefixes { get; } = new[] {"pageleft", "pageright"};

    public override ICommand CreateCommand(CommandObject commandObject)
    {
        var pageFrom = int.Parse(commandObject.CommandText.Split(':')[1]);
        return new SwitchKeyboardCommand(pageFrom, commandObject.CommandText, 10);
    }
}