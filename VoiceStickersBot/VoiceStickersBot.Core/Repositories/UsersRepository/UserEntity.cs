using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoiceStickersBot.Core.StickerPacksRepository;
using VoiceStickersBot.Infra.DatabaseTable;

namespace VoiceStickersBot.Core.UsersRepository;

[Entity]
[Table("users")]
public class UserEntity
{
    [Key] [Column("id")] public string Id { get; set; }
    
    [Column("text")] public string Text { get; set; }

//    public List<StickerPackEntity> StickerPacks { get; set; }
}