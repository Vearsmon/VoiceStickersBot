using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

namespace VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

public class AddStickerCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Добавить стикер", "AS" };
    
    private readonly Dictionary<AddStickerStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public AddStickerCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<AddStickerStepName, Func<QueryContext, ICommandArguments>>()
        {
            { AddStickerStepName.SwKbdPc, BuildAddStickerSwitchKeyboardPacksCommandArguments},
            { AddStickerStepName.Cancel, q => new AddStickerCancelArguments()},
            { AddStickerStepName.SwKbdSt, BuildAddStickerSwitchKeyboardStickersCommandArguments},
            { AddStickerStepName.SendSticker, BuildAddStickerSendStickerCommandArguments},
            { AddStickerStepName.SendInstructions, BuildAddStickerSendInstructionsCommandArguments },
            { AddStickerStepName.AddSticker, BuildAddStickerAddStickerCommandArguments}
        };
    }

    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out AddStickerStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");
        
        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildAddStickerSwitchKeyboardPacksCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 4;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!int.TryParse(queryContext.CommandArguments[0], out var pageFrom) || pageFrom < 0)
            throw new ArgumentException(
                "Invalid argument at index 0. Should be positive int.");
        
        if (!Enum.TryParse(queryContext.CommandArguments[1], out PageChangeDirection direction))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be PageChangeDirection.");
        
        if (!int.TryParse(queryContext.CommandArguments[2], out var packsOnPage) || packsOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 2. Should be positive int.");
        
        if (!long.TryParse(queryContext.CommandArguments[3], out var chatId))
            throw new ArgumentException(
                "Invalid argument at index 3. Should be long.");

        return new AddStickerSwitchKeyboardPacksArguments(
            pageFrom,
            direction,
            packsOnPage,
            chatId,
            queryContext.BotMessageId);
    }
    
    private ICommandArguments BuildAddStickerSwitchKeyboardStickersCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 5;
        
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
        
        if (!int.TryParse(queryContext.CommandArguments[3], out var stickersOnPage) || stickersOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 3. Should be positive int.");
        
        if (!long.TryParse(queryContext.CommandArguments[4], out var chatId))
            throw new ArgumentException(
                "Invalid argument at index 4. Should be long.");

        return new AddStickerSwitchKeyboardStickersArguments(
            stickerPackId,
            pageFrom,
            direction,
            stickersOnPage,
            chatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildAddStickerSendStickerCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 3;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!Guid.TryParse(queryContext.CommandArguments[1], out var stickerId))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be Guid.");
        
        if (!long.TryParse(queryContext.CommandArguments[2], out var chatId))
            throw new ArgumentException(
                "Invalid argument at index 2. Should be long.");
        
        return new AddStickerSendStickerArguments(
            stickerPackId,
            stickerId,
            chatId);
    }
    
    private ICommandArguments BuildAddStickerSendInstructionsCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!long.TryParse(queryContext.CommandArguments[0], out var chatId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be long.");
        
        return new AddStickerSendInstructionsArguments(chatId);
    }
    
    private ICommandArguments BuildAddStickerAddStickerCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 3;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (queryContext.CommandArguments[1].Length == 0)
            throw new ArgumentException(
                "Invalid argument at index 1. Should be non empty string.");
        
        if (queryContext.CommandArguments[2].Length == 0)
            throw new ArgumentException(
                "Invalid argument at index 2. Should be non empty string.");
        
        if (!long.TryParse(queryContext.CommandArguments[3], out var chatId))
            throw new ArgumentException(
                "Invalid argument at index 3. Should be long.");
        
        return new AddStickerAddStickerArguments(
            stickerPackId,
            queryContext.CommandArguments[1],
            queryContext.CommandArguments[2],
            chatId);
    }
}