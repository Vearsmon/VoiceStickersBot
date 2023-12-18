namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public interface ICommandFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; }
    public ICommand CreateCommand(RequestContext requestContext);
}