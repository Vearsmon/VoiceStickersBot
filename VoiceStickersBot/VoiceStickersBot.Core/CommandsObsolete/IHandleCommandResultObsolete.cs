namespace VoiceStickersBot.Core.Commands;

public interface IHandleCommandResultObsolete
{
    ICommandResultObsoleteObsolete ResultObsoleteObsolete { get; set; }
    bool EnsureSuccess { get; set; }
    Exception Error { get; set; }
}