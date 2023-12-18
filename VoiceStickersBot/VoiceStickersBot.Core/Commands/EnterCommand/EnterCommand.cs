namespace VoiceStickersBot.Core.Commands.EnterCommand;

public class EnterCommand : ICommand
{
    public Type CommandType => typeof(EnterCommand);
    public RequestContext RequestContext { get; }
    
    public EnterCommand(RequestContext requestContext)
    {
        RequestContext = requestContext;
    }
}