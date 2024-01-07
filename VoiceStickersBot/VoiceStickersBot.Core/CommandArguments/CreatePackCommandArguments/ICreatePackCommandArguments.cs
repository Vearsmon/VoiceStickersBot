namespace VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;

public interface ICreatePackCommandArguments : ICommandArguments
{
    public CreatePackStepName StepName { get; }
}