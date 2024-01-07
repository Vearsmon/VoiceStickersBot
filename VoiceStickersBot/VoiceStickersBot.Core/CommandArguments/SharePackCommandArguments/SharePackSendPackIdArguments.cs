namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackSendPackIdArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.SharePack;
    public SharePackStepName StepName => SharePackStepName.SendPackId;
    
    public Guid StickerPackId { get; }
    public long ChatId { get; }
    
    public SharePackSendPackIdArguments(Guid stickerPackId, long chatId)
    {
        StickerPackId = stickerPackId;
        ChatId = chatId;
    }
}