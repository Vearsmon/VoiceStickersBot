using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
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
        ICommandArguments commandArguments = null;
        try
        {
            commandArguments = commandArgumentsFactories[queryContext.CommandType].CreateCommand(queryContext);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Неизвестная команда бро");
        }
        return commandArguments;
    }
    
}