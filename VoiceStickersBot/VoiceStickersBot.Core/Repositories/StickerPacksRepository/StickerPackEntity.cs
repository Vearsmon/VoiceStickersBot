using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.ChatsRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;
using VoiceStickersBot.Infra.DatabaseTable;

#pragma warning disable CS8618

namespace VoiceStickersBot.Core.Repositories.StickerPacksRepository;

[Entity]
[Table("sticker_packs")]
[Index(nameof(Name), nameof(Id))]
internal class StickerPackEntity
{
    [Key] [Column("id")] public Guid Id { get; set; }

    [Column("owner_id")] public string OwnerId { get; set; }

    [Column("name")] public string? Name { get; set; }

    public List<StickerEntity>? Stickers { get; set; }

    //For table connection
    public List<ChatEntity>? Chats { get; set; }

    //For table connection
    public List<UserEntity> Users { get; set; }

    public StickerPack ToStickerPack()
    {
        return new StickerPack(
            Id,
            OwnerId,
            Name,
            Stickers?.Select(sticker => sticker.ToSticker()).ToList());
    }
}