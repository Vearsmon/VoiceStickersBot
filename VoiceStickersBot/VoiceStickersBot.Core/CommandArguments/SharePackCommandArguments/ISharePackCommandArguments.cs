namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public interface ISharePackCommandArguments : ICommandArguments
{
    public SharePackStepName StepName { get; }
}