using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.Repositories.UsersRepository;

public interface IUsersRepository
{
    Task Create(string id);

    Task<List<StickerPack>> GetStickerPacksOwned(string id, bool includeStickers);
    Task<(bool, List<StickerPack>?)> TryGetStickerPacksOwned(string id, bool includeStickers);
}