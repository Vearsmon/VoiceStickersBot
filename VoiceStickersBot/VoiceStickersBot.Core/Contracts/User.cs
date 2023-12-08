namespace VoiceStickersBot.Core.Contracts;

public class User
{
    public string Id { get; }

    public List<StickerPack>? StickerPacks { get; private set; }

    public User(
        string id,
        List<StickerPack>? stickerPacks)
    {
        Id = id;
        StickerPacks = stickerPacks;
    }
}