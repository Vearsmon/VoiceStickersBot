using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Infra.DatabaseTable;

namespace VoiceStickersBot.Core.Repositories.StickersRepository;

[Entity]
[Table("stickers")]
public class StickerEntity
{
    [Key] [Column("id")] public Guid Id { get; set; }

    [Column("Name")] public string? Name { get; set; }

    //TODO: Жду когда илья сделает хранение аудиофайлов
    [Column("location")] public string Location { get; set; } = null!;

    [Column("sticker_pack_id")] public Guid StickerPackId { get; set; }
    public StickerPackEntity? StickerPack { get; set; }
}