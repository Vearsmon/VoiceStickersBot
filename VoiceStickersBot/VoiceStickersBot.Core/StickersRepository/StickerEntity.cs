using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoiceStickersBot.Core.StickerPacksRepository;
using VoiceStickersBot.Infra.DatabaseTable;

namespace VoiceStickersBot.Core.StickersRepository;

[Entity]
[Table("stickers")]
public class StickerEntity
{
    [Key] [Column("id")] public Guid Id { get; set; }

    [Column("location")] public string Location { get; set; }

    [Column("sticker_pack_id")] public Guid StickerPackId { get; set; }
    public StickerPackEntity StickerPack { get; set; }
}