using Telegram.Bot;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.TgGateway.CommandResultHandlers;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public class AddStickerCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Добавить стикер", "AS" };
    
    private readonly Dictionary<AddStickerStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;
    private readonly ITelegramBotClient bot;

    public AddStickerCommandArgumentsFactory(ITelegramBotClient bot)
    {
        this.bot = bot;
        
        stepCommandBuilders = new Dictionary<AddStickerStepName, Func<QueryContext, ICommandArguments>>()
        {
            { AddStickerStepName.SwKbdPc, BuildAddStickerSwitchKeyboardPacksArguments},
            { AddStickerStepName.Cancel, q => new AddStickerCancelArguments()},
            { AddStickerStepName.SwKbdSt, BuildAddStickerSwitchKeyboardStickersArguments},
            { AddStickerStepName.SendSticker, BuildAddStickerSendStickerArguments},
            { AddStickerStepName.SendNameInstr, BuildAddStickerSendNameInstructionsArguments },
            { AddStickerStepName.SendFileInstr, BuildAddStickerSendFileInstructionsArguments},
            { AddStickerStepName.AddSticker, BuildAddStickerAddStickerArguments}
        };
    }

    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out AddStickerStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");
        
        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildAddStickerSwitchKeyboardPacksArguments(QueryContext queryContext)
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

        return new AddStickerSwitchKeyboardPacksArguments(
            pageFrom,
            direction,
            packsOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }
    
    private ICommandArguments BuildAddStickerSwitchKeyboardStickersArguments(QueryContext queryContext)
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
        
        return new AddStickerSwitchKeyboardStickersArguments(
            stickerPackId,
            pageFrom,
            direction,
            stickersOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildAddStickerSendStickerArguments(QueryContext queryContext)
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
        
        return new AddStickerSendStickerArguments(
            stickerPackId,
            stickerId,
            queryContext.ChatId);
    }
    
    private ICommandArguments BuildAddStickerSendNameInstructionsArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        return new AddStickerSendNameInstructionsArguments(stickerPackId, queryContext.ChatId);
    }
    
    private ICommandArguments BuildAddStickerSendFileInstructionsArguments(QueryContext queryContext)
    {
        const int argumentsCount = 2;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");

        var stickerName = queryContext.CommandArguments[1];
        if (stickerName.Length == 0)
            throw new ArgumentException(
                "Invalid argument at index 1. Should be Guid.");
        
        return new AddStickerSendFileInstructionsArguments(stickerPackId, stickerName, queryContext.ChatId);
    }
    
    private ICommandArguments BuildAddStickerAddStickerArguments(QueryContext queryContext)
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

        var fileId = queryContext.CommandArguments[2];
        if (fileId.Length == 0)
            throw new ArgumentException(
                "Invalid argument at index 2. Should be non empty string.");
        
        var stream = new MemoryStream();
        bot.GetInfoAndDownloadFileAsync(fileId, stream)
            .GetAwaiter()
            .GetResult();
        
        return new AddStickerAddStickerArguments(
            stickerPackId,
            queryContext.CommandArguments[1],
            stream,
            queryContext.ChatId);
    }
}