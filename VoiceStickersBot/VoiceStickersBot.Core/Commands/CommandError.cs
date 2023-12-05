namespace VoiceStickersBot.Core.Commands;

public class CommandError : Exception
{
    public CommandError(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public int ErrorCode { get; }
    public string ErrorMessage { get; }
}