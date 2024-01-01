namespace VoiceStickersBot.Core.Repositories.RepositoryExceptions;

public class UserNotFoundException : EntityNotFoundException
{
    public UserNotFoundException(string message)
        : base(message)
    {
    }
}