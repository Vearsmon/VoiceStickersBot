using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSendStickerCommandArguments : ICommandArguments<ShowAllStepName>
{
    public CommandType CommandType => CommandType.ShowAllCommand;

    public ShowAllStepName StepName => ShowAllStepName.SendSticker;

    public RequestContext<ShowAllStepName> RequestContext { get; }

    public ShowAllSendStickerCommandArguments(RequestContext<ShowAllStepName> requestContext)
    {
        RequestContext = requestContext;
    }
}