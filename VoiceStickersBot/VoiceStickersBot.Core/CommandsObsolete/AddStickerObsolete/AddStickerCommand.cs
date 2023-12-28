using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandsObsolete.AddStickerObsolete;

public class AddStickerCommand : ICommandObsolete
{
    public Type CommandType => typeof(AddStickerCommand);
    public RequestContextObsolete RequestContext { get; }
    
    public AddStickerCommand(RequestContextObsolete requestContext)
    {
        RequestContext = requestContext;
    }
}