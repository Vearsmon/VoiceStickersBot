using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Infra.DatabaseTable;

#pragma warning disable CS8618

namespace VoiceStickersBot.Core.Repositories.StickersRepository;

[Entity]
[Table("stickers")]
[PrimaryKey("Id", "StickerPackId")]
[Index(nameof(Name), nameof(Id))]
internal class StickerEntity
{
    [Column("id")] public Guid Id { get; set; }

    [Column("sticker_pack_id")] public Guid StickerPackId { get; set; }

    [Column("Name")] public string? Name { get; set; }

    [Column("location")] public string Location { get; set; }

    public StickerPackEntity StickerPack { get; set; }

    public Sticker ToSticker()
    {
        return new Sticker(
            Id,
            Name,
            Location,
            StickerPackId);
    }
}