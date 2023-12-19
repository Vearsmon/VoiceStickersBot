using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

public class ShowAllCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Показать все", "/show_all" };
    private readonly Dictionary<ShowAllStepName, Func<RequestContext, ICommandArguments>> stepCommandBuilders;

    public ShowAllCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<ShowAllStepName, Func<RequestContext, ICommandArguments>>
        {
            { ShowAllStepName.SwitchKeyboardPacks, r => new ShowAllSwitchKeyboardPacksCommandArguments() },
            { ShowAllStepName.Cancel, r => new ShowAllCancelCommandArguments() },
            { ShowAllStepName.SwitchKeyboardStickers, BuildShowAllSwitchKeyboardStickersCommandArguments },
            { ShowAllStepName.SendSticker, r => new ShowAllSendStickerCommandArguments() }
        };
    }

    public ICommandArguments CreateCommand(RequestContext requestContext)
    {
        if (!Enum.TryParse(requestContext.CommandStep, out ShowAllStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{requestContext.CommandStep}] for {nameof(ShowAllCommand)}");

        return stepCommandBuilders[stepName](requestContext);
    }

    private ICommandArguments BuildShowAllSwitchKeyboardStickersCommandArguments(RequestContext requestContext)
    {
        const int argumentsCount = 1;
        if (requestContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{requestContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(requestContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");

        return new ShowAllSwitchKeyboardStickersCommandArguments(stickerPackId);
    }
}