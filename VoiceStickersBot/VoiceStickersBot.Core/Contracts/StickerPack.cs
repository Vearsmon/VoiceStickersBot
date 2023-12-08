namespace VoiceStickersBot.Core.Contracts;

public class StickerPack
{
    public Guid Id { get; }

    public string OwnerId { get; }

    public string? Name { get; private set; }

    public List<Sticker>? Stickers { get; private set; }

    public StickerPack(
        Guid id,
        string ownerId,
        string? name,
        List<Sticker>? stickers)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
        Stickers = stickers;
    }
}