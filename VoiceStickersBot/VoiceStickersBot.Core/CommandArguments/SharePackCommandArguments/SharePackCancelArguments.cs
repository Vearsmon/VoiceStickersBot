namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackCancelArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.SharePack;

    public SharePackStepName StepName => SharePackStepName.Cancel;
}