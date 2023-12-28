namespace VoiceStickersBot.Core.Commands;

public class HandleCommandResultObsolete : IHandleCommandResultObsolete
{
    public ICommandResultObsoleteObsolete ResultObsoleteObsolete { get; set; }
    public bool EnsureSuccess { get; set; }
    public Exception Error { get; set; }
    
    public HandleCommandResultObsolete(ICommandResultObsoleteObsolete resultObsoleteObsolete, Exception? error)
    {
        if (error is null)
            EnsureSuccess = true;
        ResultObsoleteObsolete = resultObsoleteObsolete;
        Error = error;
    }
}