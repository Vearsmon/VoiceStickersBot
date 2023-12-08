using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.Repositories.ChatsRepository;

public interface IChatsRepository
{
    Task Create(string id);

    Task<List<StickerPack>> GetStickerPacksAvailable(string id, bool includeStickers);
}