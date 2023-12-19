namespace VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

public interface ICommandArgumentsFactory<StepNameType>
{
    public IReadOnlyList<string> CommandPrefixes { get; }
    public ICommandArguments<StepNameType> CreateCommand(RequestContext<StepNameType> requestContext);
}