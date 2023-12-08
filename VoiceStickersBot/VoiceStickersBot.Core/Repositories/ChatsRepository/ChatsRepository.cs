using Microsoft.EntityFrameworkCore;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Infra.VsbDatabaseCluster;

namespace VoiceStickersBot.Core.Repositories.ChatsRepository;

public class ChatsRepository : IChatsRepository
{
    private readonly IVsbDatabaseCluster vsbDatabaseCluster;

    public ChatsRepository(IVsbDatabaseCluster vsbDatabaseCluster)
    {
        this.vsbDatabaseCluster = vsbDatabaseCluster;
    }

    public async Task Create(string id)
    {
        using var table = vsbDatabaseCluster.GetTable<ChatEntity>();
        await table.PerformCreateRequestAsync(
                new ChatEntity
                {
                    Id = id
                },
                new CancellationToken())
            .ConfigureAwait(false);
    }

    public async Task<List<StickerPack>> GetStickerPacksAvailable(
        string id,
        bool includeStickers = false)
    {
        using var table = vsbDatabaseCluster.GetTable<ChatEntity>();
        var chats = await table.PerformReadonlyRequestAsync(
                r => r
                    .Where(user => user.Id == id)
                    .Include(user => user.StickerPacks)!
                    .IncludeStickers(includeStickers),
                new CancellationToken())
            .ConfigureAwait(false);

        return chats.Single()
            .StickerPacks?
            .Select(stickerPack => stickerPack.ToStickerPack())
            .ToList() ?? new List<StickerPack>();
    }
}