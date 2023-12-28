namespace VoiceStickersBot.Core.Commands;

public class CommandErrorObsolete : Exception
{
    public CommandErrorObsolete(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public int ErrorCode { get; }
    public string ErrorMessage { get; }
}