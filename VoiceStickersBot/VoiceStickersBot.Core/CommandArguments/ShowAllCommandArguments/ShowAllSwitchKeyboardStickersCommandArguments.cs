using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSwitchKeyboardStickersCommandArguments : ICommandArguments<ShowAllStepName>
{
    public CommandType CommandType => CommandType.ShowAllCommand;

    public ShowAllStepName StepName => ShowAllStepName.SwitchKeyboardStickers;

    public RequestContext<ShowAllStepName> RequestContext { get; }

    public ShowAllSwitchKeyboardStickersCommandArguments(RequestContext<ShowAllStepName> requestContext)
    {
        RequestContext = requestContext;
    }
}