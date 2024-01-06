namespace VoiceStickersBot.Core.CommandResults;

public interface IHandleCommandResult
{
    ICommandResult Result { get; set; }
    bool EnsureSuccess { get; set; }
    Exception Error { get; set; }
}
// на подумать*