namespace VoiceStickersBot.Core.Contracts;

public class Sticker
{
    public Guid Id { get; }

    public string? Name { get; }
    
    public string Location { get; }

    public Guid StickerPackId { get; }

    public Sticker(
        Guid id,
        string? name,
        string location,
        Guid stickerPackId)
    {
        Id = id;
        Name = name;
        Location = location;
        StickerPackId = stickerPackId;
    }
}