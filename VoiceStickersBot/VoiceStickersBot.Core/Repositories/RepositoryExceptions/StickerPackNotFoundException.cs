namespace VoiceStickersBot.Core.Repositories.RepositoryExceptions;

public class StickerPackNotFoundException : EntityNotFoundException
{
    public StickerPackNotFoundException(string message) : base(message)
    {
    }
}