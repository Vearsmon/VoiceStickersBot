namespace VoiceStickersBot.Core.Repositories.RepositoryExceptions;

public class StickerNotFoundException : EntityNotFoundException
{
    public StickerNotFoundException(string message) : base(message)
    {
    }
}