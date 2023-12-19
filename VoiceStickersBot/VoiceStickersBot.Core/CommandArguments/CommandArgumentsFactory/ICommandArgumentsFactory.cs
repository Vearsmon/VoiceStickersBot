namespace VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

public interface ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; }
    public ICommandArguments CreateCommand(RequestContext requestContext);
}