namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public interface ICommandFactoryObsolete
{
    public IReadOnlyList<string> CommandPrefixes { get; }
    public ICommandObsolete CreateCommand(RequestContextObsolete requestContext);
}