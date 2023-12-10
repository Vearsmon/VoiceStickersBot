namespace VoiceStickersBot.Core.Commands;

public interface ICommandResult
{
    IHandleCommandResult Result { get; set; }
    bool EnsureSuccess { get; set; }
    Exception Error { get; set; }
}