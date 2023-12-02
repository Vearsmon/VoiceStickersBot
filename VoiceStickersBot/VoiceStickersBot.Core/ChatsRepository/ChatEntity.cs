using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoiceStickersBot.Core.StickerPacksRepository;
using VoiceStickersBot.Infra.DatabaseTable;

namespace VoiceStickersBot.Core;

[Entity]
[Table("chats")]
public class ChatEntity
{
    [Key] [Column("id")] public string Id { get; set; }

    public List<StickerPackEntity> StickerPacks { get; set; }
}