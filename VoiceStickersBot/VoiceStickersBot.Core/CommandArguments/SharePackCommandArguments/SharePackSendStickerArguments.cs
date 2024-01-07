namespace VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

public class SharePackSendStickerArguments : ISharePackCommandArguments
{
    public CommandType CommandType => CommandType.SharePack;

    public SharePackStepName StepName => SharePackStepName.SendSticker;
    public Guid StickerPackId { get; }
    public Guid StickerId { get; }
    public long ChatId { get; }
    
    public SharePackSendStickerArguments(Guid stickerPackId, Guid stickerId, long chatId)
    {
        StickerPackId = stickerPackId;
        StickerId = stickerId;
        ChatId = chatId;
    }
}