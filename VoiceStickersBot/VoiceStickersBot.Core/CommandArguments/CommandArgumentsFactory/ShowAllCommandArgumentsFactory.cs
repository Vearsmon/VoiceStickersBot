using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

namespace VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

public class ShowAllCommandArgumentsFactory : ICommandArgumentsFactory<ShowAllStepName>
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Показать все", "/show_all" };
    private Dictionary<ShowAllStepName, Func<RequestContext<ShowAllStepName>, ICommandArguments<ShowAllStepName>>> StepCommands { get; } 
        = new ()
        {
            { ShowAllStepName.SwitchKeyboardPacks, r => new ShowAllSwitchKeyboardPacksCommandArguments(r) },
            { ShowAllStepName.Cancel, r => new ShowAllCancelCommandArguments(r) },
            { ShowAllStepName.SwitchKeyboardStickers, r => new ShowAllSwitchKeyboardStickersCommandArguments(r) },
            { ShowAllStepName.SendSticker, r => new ShowAllSendStickerCommandArguments(r) }
        };

    public ICommandArguments<ShowAllStepName> CreateCommand(RequestContext<ShowAllStepName> requestContext)
    {
        return StepCommands[requestContext.StepName](requestContext);
    }
}