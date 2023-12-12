namespace VoiceStickersBot.Core.Commands;

public class HandleCommandResult : IHandleCommandResult
{
    public HandleCommandResult(ICommandResult result, Exception? error)
    {
        if (error is null)
            EnsureSuccess = true;
        Result = result;
        Error = error;
    }
    
    public ICommandResult Result { get; set; }
    public bool EnsureSuccess { get; set; }
    public Exception Error { get; set; }
    
}