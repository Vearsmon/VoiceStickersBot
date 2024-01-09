using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CancelCommandArguments;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public class CancelCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Cancel" };
    private readonly Dictionary<CancelStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public CancelCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<CancelStepName, Func<QueryContext, ICommandArguments>>()
        {
            {
                CancelStepName.Cancel, q => 
                    new CacncelCancelArguments(q.ChatType, q.ChatId) 
            }
        };
    }
    
    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out CancelStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");

        return stepCommandBuilders[stepName](queryContext);
    }
}