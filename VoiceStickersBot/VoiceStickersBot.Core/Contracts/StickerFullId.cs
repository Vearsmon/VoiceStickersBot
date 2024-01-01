namespace VoiceStickersBot.Core.Contracts;

public class StickerFullId
{
    public Guid StickerId { get; }

    public Guid StickerPackId { get; }

    public StickerFullId(Guid stickerId, Guid stickerPackId)
    {
        StickerId = stickerId;
        StickerPackId = stickerPackId;
    }
}