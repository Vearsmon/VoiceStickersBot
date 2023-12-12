namespace VoiceStickersBot.Core.Commands;

public class CommandObject
{
    public string CommandText { get; }
    public RequestContex RequestContex { get; }

    public CommandObject(string commandText, RequestContex requestContex)
    {
        CommandText = commandText;
        RequestContex = requestContex;
    }
}