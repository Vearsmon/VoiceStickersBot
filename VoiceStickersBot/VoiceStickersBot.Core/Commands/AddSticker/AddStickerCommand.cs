namespace VoiceStickersBot.Core.Commands.AddSticker;

public class AddStickerCommand : ICommand
{
    public Type CommandType => typeof(AddStickerCommand);
    public RequestContext RequestContext { get; }
    
    public AddStickerCommand(RequestContext requestContext)
    {
        RequestContext = requestContext;
    }
}