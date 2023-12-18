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

    public Task<List<StickerPack>> GetStickerPacksAvailable(string id, bool includeStickers)
    {
        throw new NotImplementedException();
    }
}