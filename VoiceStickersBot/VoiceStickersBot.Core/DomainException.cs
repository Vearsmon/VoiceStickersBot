namespace VoiceStickersBot.Core.Repositories.RepositoryExceptions;

public abstract class DomainException : Exception
{
    public int Code { get; protected set; }

    protected DomainException(int code, string message)
        : base(message)
    {
        Code = code;
    }
}