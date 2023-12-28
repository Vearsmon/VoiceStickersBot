namespace VoiceStickersBot.Core.Commands;

public interface ICommandObsolete
{
    public Type CommandType { get; }
    public RequestContextObsolete RequestContext { get; }
}