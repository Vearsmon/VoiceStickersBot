using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoiceStickersBot.Core.StickersRepository;
using VoiceStickersBot.Infra.DatabaseTable;

namespace VoiceStickersBot.Core.StickerPacksRepository;

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