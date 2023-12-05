using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoiceStickersBot.Core.Repositories.ChatsRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Infra.DatabaseTable;

namespace VoiceStickersBot.Core.Repositories.StickerPacksRepository;

[Entity]
[Table("sticker_packs")]
public class StickerPackEntity
{
    [Key] [Column("id")] public Guid Id { get; set; }

    [Column("name")] public string? Name { get; set; }

    [Column("owner_id")] public Guid OwnerId { get; set; }

    public List<ChatEntity>? Chats { get; set; }

    public List<StickerEntity>? Stickers { get; set; }
}