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
            { ShowAllStepName.SwitchKeyboardPacks, BuildShowAllSwitchKeyboardPacksCommandArguments },
            { ShowAllStepName.Cancel, r => new ShowAllCancelCommandArguments() },
            { ShowAllStepName.SwitchKeyboardStickers, BuildShowAllSwitchKeyboardStickersCommandArguments },
            { ShowAllStepName.SendSticker, BuildShowAllSendStickerCommandArguments }
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
        const int argumentsCount = 4;
        if (requestContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{requestContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(requestContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!int.TryParse(requestContext.CommandArguments[1], out var pageFrom) || pageFrom < 0)
            throw new ArgumentException(
                "Invalid argument at index 1. Should be positive int.");

        if (!Enum.TryParse(requestContext.CommandArguments[2], out PageChangeDirection direction))
            throw new ArgumentException(
                "Invalid argument at index 2. Should be PageChangeDirection.");
        
        if (!int.TryParse(requestContext.CommandArguments[3], out var stickersOnPage)|| stickersOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 3. Should be positive int.");

        return new ShowAllSwitchKeyboardStickersCommandArguments(stickerPackId, pageFrom, direction, stickersOnPage);
    }
    
    private ICommandArguments BuildShowAllSwitchKeyboardPacksCommandArguments(RequestContext requestContext)
    {
        const int argumentsCount = 4;
        if (requestContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{requestContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!long.TryParse(requestContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be long.");
        
        if (!int.TryParse(requestContext.CommandArguments[1], out var pageFrom) || pageFrom < 0)
            throw new ArgumentException(
                "Invalid argument at index 1. Should be positive int.");

        if (!Enum.TryParse(requestContext.CommandArguments[2], out PageChangeDirection direction))
            throw new ArgumentException(
                "Invalid argument at index 2. Should be PageChangeDirection.");
        
        if (!int.TryParse(requestContext.CommandArguments[3], out var stickersOnPage)|| stickersOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 3. Should be positive int.");

        return new ShowAllSwitchKeyboardPacksCommandArguments(stickerPackId.ToString(), pageFrom, direction, stickersOnPage);
    }
    
    private ICommandArguments BuildShowAllSendStickerCommandArguments(RequestContext requestContext)
    {
        const int argumentsCount = 2;
        if (requestContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{requestContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(requestContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!Guid.TryParse(requestContext.CommandArguments[1], out var stickerId))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be Guid.");

        return new ShowAllSendStickerCommandArguments(stickerPackId, stickerId);
    }
}