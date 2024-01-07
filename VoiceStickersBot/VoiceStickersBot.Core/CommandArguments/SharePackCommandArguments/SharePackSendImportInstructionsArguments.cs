namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackSendImportInstructionsArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.SharePack;

    public SharePackStepName StepName => SharePackStepName.SendImportInstr;
    public long ChatId { get; }

    public SharePackSendImportInstructionsArguments(long chatId)
    {
        ChatId = chatId;
    }
}