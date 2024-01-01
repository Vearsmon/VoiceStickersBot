namespace VoiceStickersBot.Core.Contracts;

public class Sticker
{
    public StickerFullId StickerFullId { get; }

    public string? Name { get; }

    public string Location { get; }

    public Sticker(
        Guid id,
        string? name,
        string location,
        Guid stickerPackId)
    {
        Name = name;
        Location = location;
        StickerFullId = new StickerFullId(id, stickerPackId);
    }
}