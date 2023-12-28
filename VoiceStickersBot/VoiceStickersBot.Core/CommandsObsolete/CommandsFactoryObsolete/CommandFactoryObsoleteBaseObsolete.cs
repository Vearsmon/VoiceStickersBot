using VoiceStickersBot.Core.Commands.ShowAll;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public abstract class CommandFactoryObsoleteBaseObsolete : ICommandFactoryObsolete
{
    public abstract IReadOnlyList<string> CommandPrefixes { get; }
    public abstract ICommandObsolete CreateCommand(RequestContextObsolete requestContext);
}