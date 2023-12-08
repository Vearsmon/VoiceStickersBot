using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Infra.DatabaseTable;

#pragma warning disable CS8618

namespace VoiceStickersBot.Core.Repositories.UsersRepository;

[Entity]
[Table("users")]
internal class UserEntity
{
    [Key] [Column("id")] public string Id { get; set; }

    public List<StickerPackEntity>? StickerPacks { get; set; }

    public User ToUser()
    {
        return new User(
            Id,
            StickerPacks?.Select(stickerPack => stickerPack.ToStickerPack()).ToList());
    }
}