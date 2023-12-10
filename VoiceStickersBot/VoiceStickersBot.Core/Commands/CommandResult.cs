namespace VoiceStickersBot.Core.Commands;

public class CommandResult : ICommandResult
{
    public CommandResult(IHandleCommandResult result, Exception? error)
    {
        if (error is null)
            EnsureSuccess = true;
        Result = result;
        Error = error;
    }
    
    public IHandleCommandResult Result { get; set; }
    public bool EnsureSuccess { get; set; }
    public Exception Error { get; set; }
    
}