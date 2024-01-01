namespace VoiceStickersBot.Core.Repositories.RepositoryExceptions;

public class ChatNotFoundException : EntityNotFoundException
{
    public ChatNotFoundException(string message) : base(message)
    {
    }
}