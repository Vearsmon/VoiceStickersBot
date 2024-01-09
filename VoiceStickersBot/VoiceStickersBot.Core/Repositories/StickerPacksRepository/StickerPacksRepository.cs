using Microsoft.EntityFrameworkCore;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.RepositoryExceptions;
using VoiceStickersBot.Core.Repositories.UsersRepository;
using VoiceStickersBot.Infra;
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
        var stickerPack = new StickerPackEntity
        {
            Id = stickerPackId,
            Name = name,
            OwnerId = ownerId
        };

        using var table = vsbDatabaseCluster.GetTable<StickerPackEntity>();
        await table.PerformCreateRequestAsync(
            stickerPack,
            new CancellationToken()
        ).ConfigureAwait(false);
        table.Dispose();

        using var usersTable = vsbDatabaseCluster.GetTable<UserEntity>();
        await usersTable.PerformUpdateRequestAsync(
            r => r
                .Include(u => u.StickerPacks)
                .Single(u => u.Id == ownerId),
            r =>
            {
                r.StickerPacks ??= new List<StickerPackEntity>();
                r.StickerPacks.Add(stickerPack);
            },
            new CancellationToken()).ConfigureAwait(false);
    }

    public async Task<StickerPack> GetStickerPackAsync(
        Guid stickerPackId,
        bool includeStickers = false)
    {
        using var table = vsbDatabaseCluster.GetTable<StickerPackEntity>();
        var stickerPacks = await table
            .PerformReadonlyRequestAsync(
                r => r
                    .Where(stickerPack => stickerPackId == stickerPack.Id)
                    .IncludeStickers(includeStickers)
                    .OrderBy(stickerPack => stickerPack.Name)
                    .ThenBy(stickerPack => stickerPack.Id),
                new CancellationToken())
            .ConfigureAwait(false);

        if (stickerPacks.IsEmpty())
            throw new StickerPackNotFoundException($"Sticker pack with id: {stickerPackId} was not found");

        return Convert(stickerPacks.Single());
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
                .OrderBy(sticker => sticker.Name)
                .ToList());
    }
}