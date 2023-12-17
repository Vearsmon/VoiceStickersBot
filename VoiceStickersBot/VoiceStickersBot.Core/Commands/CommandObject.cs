namespace VoiceStickersBot.Core.Commands;

public class CommandObject
{
    public string CommandText { get; }
    public RequestContext RequestContext { get; }

    public CommandObject(string commandText, RequestContext requestContext)
    {
        CommandText = commandText;
        RequestContext = requestContext;
    }
}