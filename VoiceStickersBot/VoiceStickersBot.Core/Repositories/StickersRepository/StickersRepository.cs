using VoiceStickersBot.Infra.VsbDatabaseCluster;

namespace VoiceStickersBot.Core.Repositories.StickersRepository;

public class StickersRepository : IStickersRepository
{
    private readonly IVsbDatabaseCluster vsbDatabaseCluster;

    public StickersRepository(IVsbDatabaseCluster vsbDatabaseCluster)
    {
        this.vsbDatabaseCluster = vsbDatabaseCluster;
    }

    public async Task CreateAsync(Guid id, string name, string location, Guid stickerPackId)
    {
        using var table = vsbDatabaseCluster.GetTable<StickerEntity>();
        await table.PerformCreateRequestAsync(
            new StickerEntity
            {
                Id = id,
                Location = location,
                Name = name,
                StickerPackId = stickerPackId
            },
            new CancellationToken()).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Guid id)
    {
        using var table = vsbDatabaseCluster.GetTable<StickerEntity>();
        await table.PerformDeletionRequestAsync(
            r => r.Where(sticker => sticker.Id == id),
            new CancellationToken()).ConfigureAwait(false);
    }
}