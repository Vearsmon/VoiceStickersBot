namespace VoiceStickersBot.Core.Commands.ShowAll;

public class ShowAllCommand : ICommand
{
    public Type CommandType => typeof(ShowAllCommand);
    public RequestContext RequestContext { get; }

    public ShowAllCommand(RequestContext requestContext)
    {
        RequestContext = requestContext;
    }

}