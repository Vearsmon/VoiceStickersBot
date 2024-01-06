using VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

namespace VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

public class DeletePackCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Удалить пак", "DP" };

    private readonly Dictionary<DeletePackStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public DeletePackCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<DeletePackStepName, Func<QueryContext, ICommandArguments>>()
        {
            { DeletePackStepName.SwKbdPc, BuildDeletePackSwitchKeyboardPacksCommandArguments},
            { DeletePackStepName.Cancel, r => new DeletePackCancelArguments()},
            { DeletePackStepName.DeletePack, BuildDeletePackDeletePackCommandArguments },
            { DeletePackStepName.Confirm, BuildDeletePackConfirmCommandArguments }
        };
    }

    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out DeletePackStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");

        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildDeletePackSwitchKeyboardPacksCommandArguments(QueryContext queryContext)
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

        return new DeletePackSwitchKeyboardPacksArguments(
            pageFrom,
            direction,
            packsOnPage,
            chatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildDeletePackDeletePackCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 2;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");        
        
        if (!long.TryParse(queryContext.CommandArguments[1], out var chatId))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be long.");

        return new DeletePackDeletePackArguments(stickerPackId, chatId);
    }

    private ICommandArguments BuildDeletePackConfirmCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 2;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!long.TryParse(queryContext.CommandArguments[1], out var chatId))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be long.");
        

        return new DeletePackConfirmArguments(stickerPackId, chatId);
    }
}