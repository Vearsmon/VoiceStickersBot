using Microsoft.EntityFrameworkCore;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.RepositoryExceptions;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Infra;
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

    public async Task<List<StickerPack>> GetStickerPacksAvailable(string id, bool includeStickers)
    {
        using var table = vsbDatabaseCluster.GetTable<ChatEntity>();
        var chats = await table.PerformReadonlyRequestAsync(
                r => r
                    .Where(chat => chat.Id == id)
                    .Include(chat => chat.StickerPacks)!
                    .IncludeStickers(includeStickers),
                new CancellationToken())
            .ConfigureAwait(false);

        if (chats.IsEmpty())
            throw new ChatNotFoundException($"Chat with id: {id} was not found");

        return chats.Single()
            .StickerPacks?
            .Select(stickerPack => stickerPack.ToStickerPack())
            .ToList() ?? new List<StickerPack>();
    }

    public async Task<(bool, List<StickerPack>?)> TryGetPacksAvailable(string id, bool includeStickers)
    {
        using var table = vsbDatabaseCluster.GetTable<ChatEntity>();
        var chats = await table.PerformReadonlyRequestAsync(
                r => r
                    .Where(chat => chat.Id == id)
                    .Include(chat => chat.StickerPacks)!
                    .IncludeStickers(includeStickers),
                new CancellationToken())
            .ConfigureAwait(false);

        if (chats.IsEmpty())
            return (false, null);

        return (true, chats.Single()
            .StickerPacks?
            .Select(stickerPack => stickerPack.ToStickerPack())
            .ToList() ?? new List<StickerPack>());
    }

    public async Task RemoveStickerPack(string chatId, Guid stickerPackId)
    {
        using var table = vsbDatabaseCluster.GetTable<ChatEntity>();

        await table.PerformUpdateRequestAsync(
                r => r
                    .Include(c => c.StickerPacks)
                    .Single(c => c.Id == chatId),
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
}