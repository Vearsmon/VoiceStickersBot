using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllCancelCommandArguments : ICommandArguments<ShowAllStepName>
{
    public CommandType CommandType => CommandType.ShowAllCommand;

    public ShowAllStepName StepName => ShowAllStepName.Cancel;

    public RequestContext<ShowAllStepName> RequestContext { get; }

    public ShowAllCancelCommandArguments(RequestContext<ShowAllStepName> requestContext)
    {
        RequestContext = requestContext;
    }
}