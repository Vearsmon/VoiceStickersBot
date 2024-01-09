using VoiceStickersBot.Core;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public class DeletePackCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Удалить пак", "DP" };

    private readonly Dictionary<DeletePackStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public DeletePackCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<DeletePackStepName, Func<QueryContext, ICommandArguments>>()
        {
            { DeletePackStepName.SwKbdPc, BuildDeletePackSwitchKeyboardPacksArguments},
            { DeletePackStepName.SwKbdSt, BuildDeletePackSwitchKeyboardStickersArguments },
            { DeletePackStepName.SendSticker, BuildDeletePackSendStickerArguments },
            { DeletePackStepName.DeletePack, BuildDeletePackDeletePackArguments },
            { DeletePackStepName.Confirm, BuildDeletePackConfirmArguments }
        };
    }

    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out DeletePackStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");

        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildDeletePackSwitchKeyboardPacksArguments(QueryContext queryContext)
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

        return new DeletePackSwitchKeyboardPacksArguments(
            pageFrom,
            direction,
            packsOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildDeletePackSwitchKeyboardStickersArguments(QueryContext queryContext)
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
        
        return new DeletePackSwitchKeyboardStickersArguments(
            stickerPackId,
            pageFrom,
            direction,
            stickersOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildDeletePackSendStickerArguments(QueryContext queryContext)
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
        
        return new DeletePackSendStickerArguments(
            stickerPackId,
            stickerId,
            queryContext.ChatId);
    }

    private ICommandArguments BuildDeletePackDeletePackArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        return new DeletePackDeletePackArguments(stickerPackId, queryContext.ChatId);
    }

    private ICommandArguments BuildDeletePackConfirmArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");

        return new DeletePackConfirmArguments(
            stickerPackId,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }
}