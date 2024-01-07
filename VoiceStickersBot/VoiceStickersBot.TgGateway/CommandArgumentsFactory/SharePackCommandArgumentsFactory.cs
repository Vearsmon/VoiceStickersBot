using VoiceStickersBot.Core;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public class SharePackCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Поделиться паком", "SP" };
    private readonly Dictionary<SharePackStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;
    
    public SharePackCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<SharePackStepName, Func<QueryContext, ICommandArguments>>
        {
            { SharePackStepName.SwKbdPc, BuildSharePackSwitchKeyboardPacksCommandArguments },
            { SharePackStepName.Cancel, r => new SharePackCancelCommandArguments() },
        };
    }
    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out SharePackStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");

        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildSharePackSwitchKeyboardPacksCommandArguments(QueryContext queryContext)
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
        
        if (!int.TryParse(queryContext.CommandArguments[2], out var stickersOnPage)|| stickersOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 2. Should be positive int.");

        return new SharePackSwitchKeyboardPacksCommandArguments(
            pageFrom, 
            direction, 
            stickersOnPage, 
            queryContext.ChatId,
            queryContext.BotMessageId);
    }
}