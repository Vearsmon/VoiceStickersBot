using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Infra.VSBApplication.Log;
using VoiceStickersBot.TgGateway.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments;

public class TgApiCommandService
{
    private readonly Dictionary<string, ICommandArgumentsFactory> commandArgumentsFactories;

    public TgApiCommandService(List<ICommandArgumentsFactory> factories)
    {
        commandArgumentsFactories = factories
            .SelectMany(f => f.CommandPrefixes
                .Select(p => (p, f)))
            .ToDictionary(t => t.p, t => t.f);
    }
    
    public ICommandArguments CreateCommandArguments(QueryContext queryContext)
    {
        var commandArguments = commandArgumentsFactories[queryContext.CommandType].CreateCommand(queryContext);
        return commandArguments;
    }
}