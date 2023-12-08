using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.Repositories.StickerPacksRepository;

public interface IStickerPacksRepository
{
    Task CreateStickerPackAsync(Guid stickerPackId, string name, string ownerId);

    Task<StickerPack> GetStickerPackAsync(Guid stickerPackId, bool includeStickers);
}