using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.RepositoryExceptions;
using VoiceStickersBot.Infra;
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

    public async Task<Sticker> GetAsync(Guid stickerPackId, Guid id)
    {
        using var table = vsbDatabaseCluster.GetTable<StickerEntity>();
        var entities = await table
            .PerformReadonlyRequestAsync(r => r
                    .Where(e => e.StickerPackId == stickerPackId && e.Id == id),
                new CancellationToken())
            .ConfigureAwait(false);

        if (entities.IsEmpty())
            throw new StickerNotFoundException($"Sticker with id: {stickerPackId}:{id} was not found");

        return entities.Single().ToSticker();
    }

    public async Task DeleteAsync(Guid stickerPackId, Guid id)
    {
        using var table = vsbDatabaseCluster.GetTable<StickerEntity>();
        await table.PerformDeletionRequestAsync(
            r => r.Where(sticker => sticker.StickerPackId == stickerPackId && sticker.Id == id),
            new CancellationToken()).ConfigureAwait(false);
    }
}