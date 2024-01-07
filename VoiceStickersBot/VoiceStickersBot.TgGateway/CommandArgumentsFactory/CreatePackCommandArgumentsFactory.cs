using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public class CreatePackCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Создать пак", "CP" };
    private readonly Dictionary<CreatePackStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public CreatePackCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<CreatePackStepName, Func<QueryContext, ICommandArguments>>()
        {
            { CreatePackStepName.SendInstructions, BuildCreatePackSendInstructionsArguments },
            { CreatePackStepName.Cancel, qc => new CreatePackCancelArguments() },
            { CreatePackStepName.AddPack, BuildCreatePackAddPackArguments }
        };
    }


    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out CreatePackStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");

        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildCreatePackSendInstructionsArguments(QueryContext queryContext)
    {
        const int argumentsCount = 0;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        return new CreatePackSendInstructionsArguments(queryContext.ChatId);
    }
    
    private ICommandArguments BuildCreatePackAddPackArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (queryContext.CommandArguments[0].Length == 0)
            throw new ArgumentException(
                "Invalid argument at index 0. Should be non empty string.");

        return new CreatePackAddPackArguments(queryContext.ChatId, queryContext.CommandArguments[0]);
    }
}