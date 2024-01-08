namespace VoiceStickersBot.Core.CommandArguments.StartCommandArguments;

public interface IStartCommandArguments : ICommandArguments
{
    public StartStepName StepName { get; }
}