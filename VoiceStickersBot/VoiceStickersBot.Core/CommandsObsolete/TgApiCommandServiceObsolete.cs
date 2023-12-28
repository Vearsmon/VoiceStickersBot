using VoiceStickersBot.Core.Commands.CommandsFactory;
using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.Core.Commands;

public class TgApiCommandServiceObsolete
{
    private readonly Dictionary<string, ICommandFactoryObsolete> commandFactories;

    public TgApiCommandServiceObsolete(List<ICommandFactoryObsolete> factories)
    {
        commandFactories = factories
            .SelectMany(f => f.CommandPrefixes
                .Select(p => (p, f)))
            .ToDictionary(t => t.p, t => t.f);
    }
    
    public ICommandObsolete CreateTextCommand(RequestContextObsolete requestContext)
    {
        ICommandObsolete command = null;
        try
        {
            command = commandFactories[requestContext.CommandText.Split('@').First()].CreateCommand(requestContext);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Неизвестная команда бро");
        }
        return command;
    }

    public ICommandObsolete CreateInlineCommand(RequestContextObsolete requestContext)
    {
        ICommandObsolete command = null;
        try
        {
            command = commandFactories[requestContext.CommandText.Split(':').First()].CreateCommand(requestContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Неизвестная команда бро");
        }
        return command;
    }
    
}