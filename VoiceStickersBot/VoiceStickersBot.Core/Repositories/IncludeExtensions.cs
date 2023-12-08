using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using VoiceStickersBot.Core.Repositories.ChatsRepository;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.Repositories;

internal static class IncludeExtensions
{
    public static IQueryable<UserEntity> IncludeStickers(
        this IIncludableQueryable<UserEntity, List<StickerPackEntity>> query,
        bool includeStickers)
    {
        return includeStickers
            ? query.ThenInclude(stickerPack => stickerPack.Stickers)
            : query;
    }

    public static IQueryable<ChatEntity> IncludeStickers(
        this IIncludableQueryable<ChatEntity, List<StickerPackEntity>> query,
        bool includeStickers)
    {
        return includeStickers
            ? query.ThenInclude(stickerPack => stickerPack.Stickers)
            : query;
    }

    public static IQueryable<StickerPackEntity> IncludeStickers(
        this IQueryable<StickerPackEntity> query,
        bool includeStickers)
    {
        return includeStickers
            ? query.Include(stickerPack => stickerPack.Stickers)
            : query;
    }
}