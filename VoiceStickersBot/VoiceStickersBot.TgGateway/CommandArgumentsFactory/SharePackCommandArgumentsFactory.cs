using VoiceStickersBot.Core;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public class SharePackCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Импорт/экспорт пака", "SP" };
    
    private readonly Dictionary<SharePackStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public SharePackCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<SharePackStepName, Func<QueryContext, ICommandArguments>>()
        {
            { SharePackStepName.Cancel, q => new SharePackCancelArguments()},
            { SharePackStepName.Choice, BuildSharePackChoiceArguments},
            { SharePackStepName.SwKbdPc, BuildSharePackSwitchKeyboardPacksArguments},
            { SharePackStepName.SwKbdSt, BuildSharePackSwitchKeyboardStickersArguments},
            { SharePackStepName.SendSticker, BuildSharePackSendStickerArguments},
            { SharePackStepName.SendImportInstr, BuildSharePackSendImportInstructionsArguments},
            { SharePackStepName.SendPackId, BuildSharePackSendPackIdArguments},
            { SharePackStepName.ImportPack, BuildSharePackImportPackArguments}
        };
    }

    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out SharePackStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");
        
        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildSharePackChoiceArguments(QueryContext queryContext)
    {
        const int argumentsCount = 0;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        return new SharePackChoiceArguments(
            queryContext.ChatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildSharePackSwitchKeyboardPacksArguments(QueryContext queryContext)
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

        return new SharePackSwitchKeyboardPacksArguments(
            pageFrom,
            direction,
            packsOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }
    
    private ICommandArguments BuildSharePackSwitchKeyboardStickersArguments(QueryContext queryContext)
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
        
        return new SharePackSwitchKeyboardStickersArguments(
            stickerPackId,
            pageFrom,
            direction,
            stickersOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildSharePackSendStickerArguments(QueryContext queryContext)
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
        
        return new SharePackSendStickerArguments(
            stickerPackId,
            stickerId,
            queryContext.ChatId);
    }
    
    private ICommandArguments BuildSharePackSendImportInstructionsArguments(QueryContext queryContext)
    {
        const int argumentsCount = 0;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        return new SharePackSendImportInstructionsArguments(queryContext.ChatId);
    }
    
    private ICommandArguments BuildSharePackSendPackIdArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        return new SharePackSendPackIdArguments(stickerPackId, queryContext.ChatId);
    }
    
    private ICommandArguments BuildSharePackImportPackArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        return new SharePackImportPackArguments(
            stickerPackId,
            queryContext.ChatId);
    }
}