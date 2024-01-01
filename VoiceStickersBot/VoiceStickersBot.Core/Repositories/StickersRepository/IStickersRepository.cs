using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.Repositories.StickersRepository;

public interface IStickersRepository
{
    Task CreateAsync(Guid id, string name, string location, Guid stickerPackId);

    Task<Sticker> GetAsync(Guid stickerPackId, Guid id);

    Task DeleteAsync(Guid id);
}