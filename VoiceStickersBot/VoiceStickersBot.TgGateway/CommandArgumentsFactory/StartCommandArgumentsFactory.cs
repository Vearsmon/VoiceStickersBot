using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.StartCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public class StartCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Start" };
    
    private readonly Dictionary<StartStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public StartCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<StartStepName, Func<QueryContext, ICommandArguments>>()
        {
            { StartStepName.Start, q => new StartStartArguments(q.ChatId) }
        };
    }
    
    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out StartStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");

        return stepCommandBuilders[stepName](queryContext);
    }
}