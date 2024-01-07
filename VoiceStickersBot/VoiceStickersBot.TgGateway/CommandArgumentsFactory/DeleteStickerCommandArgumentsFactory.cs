using VoiceStickersBot.Core;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public class DeleteStickerCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Удалить стикер", "DS" };
    
    private readonly Dictionary<DeleteStickerStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public DeleteStickerCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<DeleteStickerStepName, Func<QueryContext, ICommandArguments>>()
        {
            { DeleteStickerStepName.SwKbdPc, BuildDeleteStickerSwitchKeyboardPacksArguments},
            { DeleteStickerStepName.SwKbdSt, BuildDeleteStickerSwitchKeyboardStickersArguments },
            { DeleteStickerStepName.SendSticker, BuildDeleteStickerSendStickerArguments },
            { DeleteStickerStepName.DeleteSticker, BuildDeleteStickerDeleteStickerArguments },
            { DeleteStickerStepName.Confirm, BuildDeleteStickerConfirmArguments }
        };
    }
    
    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out DeleteStickerStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");

        return stepCommandBuilders[stepName](queryContext);
    }
    
    private ICommandArguments BuildDeleteStickerSwitchKeyboardPacksArguments(QueryContext queryContext)
    {
        const int argumentsCount = 3;
        
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

        return new DeleteStickerSwitchKeyboardPacksArguments(
            pageFrom,
            direction,
            packsOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildDeleteStickerSwitchKeyboardStickersArguments(QueryContext queryContext)
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
        
        if (!int.TryParse(queryContext.CommandArguments[3], out var stickersOnPage) || stickersOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 3. Should be positive int.");
        
        return new DeleteStickerSwitchKeyboardStickersArguments(
            stickerPackId,
            pageFrom,
            direction,
            stickersOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildDeleteStickerSendStickerArguments(QueryContext queryContext)
    {
        const int argumentsCount = 2;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!Guid.TryParse(queryContext.CommandArguments[1], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be Guid.");
        
        return new DeleteStickerSendStickerArguments(
            stickerPackId,
            stickerId,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildDeleteStickerDeleteStickerArguments(QueryContext queryContext)
    {
        const int argumentsCount = 2;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!Guid.TryParse(queryContext.CommandArguments[1], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be Guid.");

        return new DeleteStickerDeleteStickerArguments(stickerPackId, stickerId, queryContext.ChatId);
    }

    private ICommandArguments BuildDeleteStickerConfirmArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");

        return new DeleteStickerConfirmArguments(
            stickerPackId,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }
}