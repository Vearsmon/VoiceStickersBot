using VoiceStickersBot.Core.Commands.ShowAll;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public abstract class CommandFactoryBase : ICommandFactory
{
    public abstract IReadOnlyList<string> CommandPrefixes { get; }
    public abstract ICommand CreateCommand(CommandObject commandObject);
}