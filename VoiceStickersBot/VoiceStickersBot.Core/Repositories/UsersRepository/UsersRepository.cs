using Microsoft.EntityFrameworkCore;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.RepositoryExceptions;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Infra;
using VoiceStickersBot.Infra.VsbDatabaseCluster;

namespace VoiceStickersBot.Core.Repositories.UsersRepository;

public class UsersRepository : IUsersRepository
{
    private readonly IVsbDatabaseCluster vsbDatabaseCluster;

    public UsersRepository(IVsbDatabaseCluster vsbDatabaseCluster)
    {
        this.vsbDatabaseCluster = vsbDatabaseCluster;
    }

    public async Task Create(string id)
    {
        using var table = vsbDatabaseCluster.GetTable<UserEntity>();
        await table.PerformCreateRequestAsync(
                new UserEntity
                {
                    Id = id
                },
                new CancellationToken())
            .ConfigureAwait(false);
    }

    public async Task<bool> CreateIfNotExists(string id)
    {
        using var table = vsbDatabaseCluster.GetTable<UserEntity>();

        var users = await table.PerformReadonlyRequestAsync(
                r => r.Where(u => u.Id == id),
                new CancellationToken())
            .ConfigureAwait(false);

        if (!users.IsEmpty())
            return false;

        await table.PerformCreateRequestAsync(
                new UserEntity { Id = id },
                new CancellationToken())
            .ConfigureAwait(false);

        return true;
    }

    public async Task<List<StickerPack>> GetStickerPacks(
        string id,
        bool includeStickers = false)
    {
        var users = await GetUser(id, includeStickers).ConfigureAwait(false);

        return users.IsEmpty()
            ? throw new UserNotFoundException($"User with id: {id} was not found")
            : ExtractStickerPacks(users);
    }

    public async Task<(bool, List<StickerPack>?)> TryGetStickerPacks(
        string id,
        bool includeStickers = false)
    {
        var users = await GetUser(id, includeStickers).ConfigureAwait(false);

        return users.IsEmpty()
            ? (false, null)
            : (true, ExtractStickerPacks(users));
    }

    public async Task AddStickerPackToUser(string userId, Guid stickerPackId)
    {
        var stickerPackTable = vsbDatabaseCluster.GetTable<StickerPackEntity>();
        var stickerPacks = await stickerPackTable.PerformReadonlyRequestAsync(
                r => r.Where(p => p.Id == stickerPackId),
                new CancellationToken())
            .ConfigureAwait(false);
        if (stickerPacks.IsEmpty())
            throw new StickerPackNotFoundException($"Sticker pack with id: {stickerPackId} was not found");

        var stickerPack = stickerPacks.Single();
        stickerPackTable.Dispose();

        using var usersTable = vsbDatabaseCluster.GetTable<UserEntity>();
        await usersTable.PerformUpdateRequestAsync(
                r => r
                    .Include(u => u.StickerPacks)
                    .Single(u => u.Id == userId),
                e => (e.StickerPacks ??= new List<StickerPackEntity>()).Add(stickerPack),
                new CancellationToken())
            .ConfigureAwait(false);
    }

    public async Task RemoveStickerPack(string userId, Guid stickerPackId)
    {
        using var table = vsbDatabaseCluster.GetTable<UserEntity>();

        await table.PerformUpdateRequestAsync(
                r => r
                    .Include(u => u.StickerPacks)
                    .Single(u => u.Id == userId),
                r =>
                {
                    r.StickerPacks ??= new List<StickerPackEntity>();
                    var stickerPackToRemove = r.StickerPacks.FirstOrDefault(p => p.Id == stickerPackId);
                    if (stickerPackToRemove is not null)
                        r.StickerPacks.Remove(stickerPackToRemove);
                },
                new CancellationToken())
            .ConfigureAwait(false);
    }

    private async Task<List<UserEntity>> GetUser(string id, bool includeStickers)
    {
        using var table = vsbDatabaseCluster.GetTable<UserEntity>();
        var users = await table.PerformReadonlyRequestAsync(
                r => r
                    .Where(user => user.Id == id)
                    .Include(user => user.StickerPacks)!
                    .IncludeStickers(includeStickers),
                new CancellationToken())
            .ConfigureAwait(false);
        return users;
    }

    private static List<StickerPack> ExtractStickerPacks(IEnumerable<UserEntity> users)
    {
        return users.Single()
            .StickerPacks?
            .Select(stickerPack => stickerPack.ToStickerPack())
            .ToList() ?? new List<StickerPack>();
    }
}