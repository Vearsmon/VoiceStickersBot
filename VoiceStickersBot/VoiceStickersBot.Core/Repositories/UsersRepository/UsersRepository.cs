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
        var stickerPacks = await GetUserStickerPacks(
                id,
                includeStickers,
                false)
            .ConfigureAwait(false);

        return stickerPacks.IsEmpty()
            ? throw new UserNotFoundException($"User with id: {id} was not found")
            : ConvertStickerPacks(stickerPacks);
    }

    public async Task<(bool, List<StickerPack>?)> TryGetStickerPacks(
        string id,
        bool includeStickers = false)
    {
        var stickerPacks = await GetUserStickerPacks(
                id,
                includeStickers,
                false)
            .ConfigureAwait(false);

        return stickerPacks.IsEmpty()
            ? (false, null)
            : (true, ConvertStickerPacks(stickerPacks));
    }

    public async Task<List<StickerPack>> GetStickerPacksOwned(string id, bool includeStickers)
    {
        var stickerPacks = await GetUserStickerPacks(
                id,
                includeStickers,
                true)
            .ConfigureAwait(false);

        return ConvertStickerPacks(stickerPacks);
    }

    public async Task AddStickerPackToUser(string userId, Guid stickerPackId)
    {
        var stickerPack = await GetStickerPack(stickerPackId).ConfigureAwait(false);
        if (stickerPack is null)
            throw new StickerPackNotFoundException($"Sticker pack with id: {stickerPackId} was not found");

        await InnerAddStickerPackToUser(userId, stickerPack).ConfigureAwait(false);
    }

    public async Task<bool> TryAddStickerPackToUser(string userId, Guid stickerPackId)
    {
        var stickerPack = await GetStickerPack(stickerPackId).ConfigureAwait(false);
        if (stickerPack is null)
            return false;

        await InnerAddStickerPackToUser(userId, stickerPack).ConfigureAwait(false);
        return true;
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

    private async Task<StickerPackEntity?> GetStickerPack(Guid stickerPackId)
    {
        using var stickerPackTable = vsbDatabaseCluster.GetTable<StickerPackEntity>();
        var stickerPacks = await stickerPackTable.PerformReadonlyRequestAsync(
                r => r.Where(p => p.Id == stickerPackId),
                new CancellationToken())
            .ConfigureAwait(false);
        return stickerPacks.FirstOrDefault();
    }

    private async Task InnerAddStickerPackToUser(string userId, StickerPackEntity stickerPack)
    {
        using var usersTable = vsbDatabaseCluster.GetTable<UserEntity>();
        await usersTable.PerformUpdateRequestAsync(
                r => r
                    .Include(u => u.StickerPacks)
                    .Single(u => u.Id == userId),
                e => (e.StickerPacks ??= new List<StickerPackEntity>()).Add(stickerPack),
                new CancellationToken())
            .ConfigureAwait(false);
    }

    private async Task<List<StickerPackEntity>> GetUserStickerPacks(
        string userId,
        bool includeStickers,
        bool ownedOnly)
    {
        using var table = vsbDatabaseCluster.GetTable<UserEntity>();
        var users = await table.PerformReadonlyRequestAsync<StickerPackEntity>(
                r => r
                    .Where(user => user.Id == userId)
                    .Include(user => user.StickerPacks)!
                    .IncludeStickers(includeStickers)
                    .SelectMany(user => user.StickerPacks!)
                    .Where(stickerPack => !ownedOnly || stickerPack.OwnerId == userId)
                    .OrderBy(stickerPack => stickerPack.Name)
                    .ThenBy(stickerPack => stickerPack.Id),
                new CancellationToken())
            .ConfigureAwait(false);
        return users;
    }

    private static List<StickerPack> ConvertStickerPacks(IEnumerable<StickerPackEntity> users)
    {
        return users
            .Select(stickerPack => stickerPack.ToStickerPack())
            .ToList();
    }
}