using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public interface IShowAllCommandArguments : ICommandArguments
{
    public ShowAllStepName StepName { get; }
}