using Microsoft.EntityFrameworkCore;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.RepositoryExceptions;
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

    public async Task<List<StickerPack>> GetStickerPacksOwned(
        string id,
        bool includeStickers = false)
    {
        var users = await GetUser(id, includeStickers).ConfigureAwait(false);

        return users.IsEmpty()
            ? throw new UserNotFoundException($"User with id: {id} was not found")
            : ExtractStickerPacks(users);
    }

    public async Task<(bool, List<StickerPack>?)> TryGetStickerPacksOwned(
        string id,
        bool includeStickers = false)
    {
        var users = await GetUser(id, includeStickers).ConfigureAwait(false);

        return users.IsEmpty()
            ? (false, null)
            : (true, ExtractStickerPacks(users));
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