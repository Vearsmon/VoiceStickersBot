namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public interface ICommandFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; }
    public ICommand CreateCommand(CommandObject commandObject);
}