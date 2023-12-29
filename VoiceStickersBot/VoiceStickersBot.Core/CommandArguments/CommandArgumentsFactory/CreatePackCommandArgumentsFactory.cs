using VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;

namespace VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

public class CreatePackCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Создать пак", "/create_pack" };
    private readonly Dictionary<CreatePackStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;

    public CreatePackCommandArgumentsFactory()
    {
        stepCommandBuilders = new Dictionary<CreatePackStepName, Func<QueryContext, ICommandArguments>>()
        {
            { CreatePackStepName.SendInstructions, BuildCreatePackSendInstructionsCommandArguments },
            { CreatePackStepName.Cancel, qc => new CreatePackCancelArguments() },
            { CreatePackStepName.AddPack, BuildCreatePackAddPackCommandArguments }
        };
    }


    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out CreatePackStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");

        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildCreatePackSendInstructionsCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!long.TryParse(queryContext.CommandArguments[0], out var chatId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be long.");

        return new CreatePackSendInstructionsArguments(chatId);
    }
    
    private ICommandArguments BuildCreatePackAddPackCommandArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!long.TryParse(queryContext.CommandArguments[0], out var chatId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be long.");
        
        if (queryContext.CommandArguments[1].Length != 0)
            throw new ArgumentException(
                "Invalid argument at index 0. Should be long.");

        return new CreatePackAddPackArguments(chatId, queryContext.CommandArguments[1]);
    }
}