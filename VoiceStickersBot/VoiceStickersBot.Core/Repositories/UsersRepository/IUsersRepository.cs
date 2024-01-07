using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.Repositories.UsersRepository;

public interface IUsersRepository
{
    Task Create(string id);

    Task<bool> CreateIfNotExists(string id);

    Task<List<StickerPack>> GetStickerPacks(string id, bool includeStickers);

    Task<(bool, List<StickerPack>?)> TryGetStickerPacks(string id, bool includeStickers);

    Task AddStickerPackToUser(string userId, Guid stickerPackId);

    Task RemoveStickerPack(string userId, Guid stickerPackId);
}