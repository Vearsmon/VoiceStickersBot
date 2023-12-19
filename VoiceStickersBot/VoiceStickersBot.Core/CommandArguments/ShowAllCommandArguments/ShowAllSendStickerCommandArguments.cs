using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSendStickerCommandArguments : ICommandArguments<ShowAllStepName>
{
    public CommandType CommandType => CommandType.ShowAll;
    public RequestContext<ShowAllStepName> RequestContext { get; }

    public ShowAllSendStickerCommandArguments(RequestContext<ShowAllStepName> requestContext)
    {
        RequestContext = requestContext;
    }
}