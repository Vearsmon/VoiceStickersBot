namespace VoiceStickersBot.Core.Repositories.RepositoryExceptions;

public class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(string message)
        : base(404, message)
    {
    }
}