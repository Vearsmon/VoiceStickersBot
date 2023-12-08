namespace VoiceStickersBot.Core.Repositories.StickersRepository;

public interface IStickersRepository
{
    Task CreateAsync(Guid id, string name, string location, Guid stickerPackId);

    Task DeleteAsync(Guid id);
}