namespace VoiceStickersBot.Core.CommandArguments.CancelCommandArguments;

public interface ICancelCommandArguments : ICommandArguments
{
    public CancelStepName StepName { get; }
}