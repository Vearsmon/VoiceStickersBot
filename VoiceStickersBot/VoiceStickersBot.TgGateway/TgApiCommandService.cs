using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Infra.VSBApplication.Log;
using VoiceStickersBot.TgGateway.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments;

public class TgApiCommandService
{
    private readonly Dictionary<string, ICommandArgumentsFactory> commandArgumentsFactories;
    private readonly ILog log;

    public TgApiCommandService(List<ICommandArgumentsFactory> factories, ILog log)
    {
        commandArgumentsFactories = factories
            .SelectMany(f => f.CommandPrefixes
                .Select(p => (p, f)))
            .ToDictionary(t => t.p, t => t.f);
        this.log = log;
    }
    
    public ICommandArguments CreateCommandArguments(QueryContext queryContext)
    {
        var commandArguments = commandArgumentsFactories[queryContext.CommandType].CreateCommand(queryContext);
        return commandArguments;
    }
}