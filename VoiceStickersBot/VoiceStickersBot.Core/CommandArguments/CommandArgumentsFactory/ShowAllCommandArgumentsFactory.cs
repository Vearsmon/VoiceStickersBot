using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

namespace VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

public class ShowAllCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Показать все", "/show_all", "SA" };
    private readonly Dictionary<ShowAllStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public ShowAllCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<ShowAllStepName, Func<QueryContext, ICommandArguments>>
        {
            { ShowAllStepName.SwKbdPc, BuildShowAllSwitchKeyboardPacksCommandArguments },
            { ShowAllStepName.Cancel, r => new ShowAllCancelCommandArguments() },
            { ShowAllStepName.SwKbdSt, BuildShowAllSwitchKeyboardStickersCommandArguments },
            { ShowAllStepName.SendSticker, BuildShowAllSendStickerCommandArguments }
        };
    }

    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out ShowAllStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");

        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildShowAllSwitchKeyboardStickersCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 4;
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!int.TryParse(queryContext.CommandArguments[1], out var pageFrom) || pageFrom < 0)
            throw new ArgumentException(
                "Invalid argument at index 1. Should be positive int.");

        if (!Enum.TryParse(queryContext.CommandArguments[2], out PageChangeDirection direction))
            throw new ArgumentException(
                "Invalid argument at index 2. Should be PageChangeDirection.");
        
        if (!int.TryParse(queryContext.CommandArguments[3], out var stickersOnPage)|| stickersOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 3. Should be positive int.");

        return new ShowAllSwitchKeyboardStickersCommandArguments(
            stickerPackId, 
            pageFrom,
            direction, 
            stickersOnPage, 
            queryContext.ChatId,
            queryContext.BotMessageId);
    }
    
    private ICommandArguments BuildShowAllSwitchKeyboardPacksCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 4;
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!long.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be long.");
        
        if (!int.TryParse(queryContext.CommandArguments[1], out var pageFrom) || pageFrom < 0)
            throw new ArgumentException(
                "Invalid argument at index 1. Should be positive int.");

        if (!Enum.TryParse(queryContext.CommandArguments[2], out PageChangeDirection direction))
            throw new ArgumentException(
                "Invalid argument at index 2. Should be PageChangeDirection.");
        
        if (!int.TryParse(queryContext.CommandArguments[3], out var stickersOnPage)|| stickersOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 3. Should be positive int.");

        return new ShowAllSwitchKeyboardPacksCommandArguments(
            stickerPackId.ToString(), 
            pageFrom, 
            direction, 
            stickersOnPage, 
            queryContext.ChatId,
            queryContext.BotMessageId);
    }
    
    private ICommandArguments BuildShowAllSendStickerCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 2;
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!Guid.TryParse(queryContext.CommandArguments[1], out var stickerId))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be Guid.");

        return new ShowAllSendStickerCommandArguments(stickerPackId, stickerId, queryContext.ChatId);
    }
}