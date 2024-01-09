namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackImportPackArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.SharePack;
    
    public SharePackStepName StepName => SharePackStepName.ImportPack;
    public Guid StickerPackId { get; }
    public string ChatType { get; }
    public long ChatId { get; }
    
    public SharePackImportPackArguments(Guid stickerPackId, string chatType, long chatId)
    {
        StickerPackId = stickerPackId;
        ChatType = chatType;
        ChatId = chatId;
    }
}