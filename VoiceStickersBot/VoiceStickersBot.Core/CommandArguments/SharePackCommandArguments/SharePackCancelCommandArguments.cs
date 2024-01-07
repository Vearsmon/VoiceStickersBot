namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackCancelCommandArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public SharePackStepName StepName => SharePackStepName.Cancel;
}