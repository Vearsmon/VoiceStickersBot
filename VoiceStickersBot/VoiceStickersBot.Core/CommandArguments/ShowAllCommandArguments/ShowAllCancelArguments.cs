using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllCancelArguments : IShowAllCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public ShowAllStepName StepName => ShowAllStepName.Cancel;
}