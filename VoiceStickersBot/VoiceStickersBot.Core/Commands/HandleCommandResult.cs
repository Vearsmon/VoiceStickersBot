namespace VoiceStickersBot.Core.Commands;

public class HandleCommandResult : IHandleCommandResult
{
    public ICommandResultObsolete ResultObsolete { get; set; }
    public bool EnsureSuccess { get; set; }
    public Exception Error { get; set; }
    
    public HandleCommandResult(ICommandResultObsolete resultObsolete, Exception? error)
    {
        if (error is null)
            EnsureSuccess = true;
        ResultObsolete = resultObsolete;
        Error = error;
    }
}