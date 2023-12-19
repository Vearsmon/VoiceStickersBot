namespace VoiceStickersBot.Core.Commands;

public interface IHandleCommandResult
{
    ICommandResultObsolete ResultObsolete { get; set; }
    bool EnsureSuccess { get; set; }
    Exception Error { get; set; }
}