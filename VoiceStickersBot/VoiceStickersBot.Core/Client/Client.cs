using System.Reflection;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.Client;

public class Client
{
    private List<ICommandHandlerFactory> factoriesList;
    public Client()
    {
        factoriesList = new List<ICommandHandlerFactory>();
        var interfaceType = typeof(ICommandHandlerFactory);
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
            .ToList();
        foreach (var type in types)
            factoriesList.Add((ICommandHandlerFactory)Activator.CreateInstance(type)!);
    }

    public ICommandResultObsolete Handle(ICommand command)
    {
        var mainHandler = new TgApiCommandHandlerService(factoriesList);

        var result = mainHandler.Handle(command);
        if (result.EnsureSuccess)
            return result.ResultObsolete;
        
        throw result.Error; //обработка ошибок
    }
}