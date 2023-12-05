using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Infra.DatabaseTable;

namespace VoiceStickersBot.Core.Repositories.UsersRepository;

[Entity]
[Table("users")]
public class UserEntity
{
    [Key] [Column("id")] public string Id { get; set; } = null!;

    public List<StickerPackEntity>? StickerPacks { get; set; }
}