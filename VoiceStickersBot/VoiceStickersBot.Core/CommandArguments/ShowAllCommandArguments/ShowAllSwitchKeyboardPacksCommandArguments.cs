using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSwitchKeyboardPacksCommandArguments : ICommandArguments<ShowAllStepName>
{
    public CommandType CommandType => CommandType.ShowAllCommand;

    public ShowAllStepName StepName => ShowAllStepName.SwitchKeyboardPacks;

    public RequestContext<ShowAllStepName> RequestContext { get; }

    public ShowAllSwitchKeyboardPacksCommandArguments(RequestContext<ShowAllStepName> requestContext)
    {
        RequestContext = requestContext;
    }
}