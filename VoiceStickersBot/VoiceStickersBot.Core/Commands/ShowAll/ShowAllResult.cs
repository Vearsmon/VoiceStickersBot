namespace VoiceStickersBot.Core.Commands.ShowAll;

public class ShowAllResult : ICommandResult
{
    public ICommandResult Result { get; set; }
    public bool EnsureSuccess { get; set; }
    public Exception Error { get; set; }
}