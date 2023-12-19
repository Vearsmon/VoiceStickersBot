using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments;

public interface ICommandArguments<StepNameType>
{
    public CommandType CommandType { get; }
    public StepNameType StepName { get; }
    public RequestContext<StepNameType> RequestContext { get; }
}