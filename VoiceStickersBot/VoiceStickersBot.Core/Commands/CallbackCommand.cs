namespace VoiceStickersBot.Core.Commands;

public class CallbackCommand : ICommand
{
    public CallbackCommand(string callbackText)
    {
        this.CallbackText = callbackText;
    }

    public string CallbackText { get; }
}