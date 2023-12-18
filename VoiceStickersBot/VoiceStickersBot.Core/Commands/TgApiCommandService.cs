using VoiceStickersBot.Core.Commands.CommandsFactory;
using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.Core.Commands;

public class TgApiCommandService
{
    private readonly Dictionary<string, ICommandFactory> commandFactories;

    public TgApiCommandService(List<ICommandFactory> factories)
    {
        commandFactories = factories
            .SelectMany(f => f.CommandPrefixes
                .Select(p => (p, f)))
            .ToDictionary(t => t.p, t => t.f);
    }
    
    public ICommand CreateTextCommand(RequestContext requestContext)
    {
        ICommand command = null;
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

    public ICommand CreateInlineCommand(RequestContext requestContext)
    {
        ICommand command = null;
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