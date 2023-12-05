using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Infra.DatabaseTable;

namespace VoiceStickersBot.Core.Repositories.ChatsRepository;

[Entity]
[Table("chats")]
public class ChatEntity
{
    [Key] [Column("id")] public string Id { get; set; }

    public List<StickerPackEntity> StickerPacks { get; set; }
}