using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Infra.VsbDatabaseCluster;

namespace VoiceStickersBot.Core.Repositories.StickerPacksRepository;

public class StickerPacksRepository : IStickerPacksRepository
{
    private readonly IVsbDatabaseCluster vsbDatabaseCluster;

    public StickerPacksRepository(IVsbDatabaseCluster vsbDatabaseCluster)
    {
        this.vsbDatabaseCluster = vsbDatabaseCluster;
    }

    public async Task CreateStickerPackAsync(Guid stickerPackId, string name, string ownerId)
    {
        using var table = vsbDatabaseCluster.GetTable<StickerPackEntity>();
        await table.PerformCreateRequestAsync(
            new StickerPackEntity
            {
                Id = stickerPackId,
                Name = name,
                OwnerId = ownerId
            },
            new CancellationToken()
        ).ConfigureAwait(false);
    }

    public async Task<StickerPack> GetStickerPackAsync(
        Guid stickerPackId,
        bool includeStickers = false)
    {
        using var table = vsbDatabaseCluster.GetTable<StickerPackEntity>();
        var stickerPackEntity = (await table
            .PerformReadonlyRequestAsync(
                r => r
                    .Where(stickerPack => stickerPackId == stickerPack.Id)
                    .IncludeStickers(includeStickers),
                new CancellationToken())
            .ConfigureAwait(false)).Single();

        return Convert(stickerPackEntity);
    }

    private static StickerPack Convert(StickerPackEntity stickerPackEntity)
    {
        return new StickerPack(
            stickerPackEntity.Id,
            stickerPackEntity.OwnerId,
            stickerPackEntity.Name,
            stickerPackEntity.Stickers?
                .Select(sticker =>
                    new Sticker(
                        sticker.Id,
                        sticker.Name,
                        sticker.Location,
                        sticker.StickerPackId))
                .ToList());
    }
}