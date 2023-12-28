using System.Reflection;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;
using VoiceStickersBot.Infra.VsbDatabaseCluster;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

namespace VoiceStickersBot.Core.Client;

public class Client
{
    private List<ICommandHandlerFactory> factoriesList;
    public Client()
    {
        var dataCluster = new PostgresVsbDatabaseCluster(new PostgresVsbDatabaseOptionsProvider());
        factoriesList = new List<ICommandHandlerFactory>() 
            { new ShowAllCommandHandlerFactory(new UsersRepository(dataCluster), new StickerPacksRepository(dataCluster))};
        /*var interfaceType = typeof(ICommandHandlerFactory);
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
            .ToList();
        foreach (var type in types)
            factoriesList.Add((ICommandHandlerFactory)Activator.CreateInstance(type)!);*/
    }

    public async Task<ICommandResult> Handle(ICommandArguments commandArguments)
    {
        var mainHandler = new TgApiCommandHandlerService(factoriesList);

        var result = await mainHandler.Handle(commandArguments).ConfigureAwait(false);
        if (result.EnsureSuccess)
            return result.Result;
        
        throw result.Error; //обработка ошибок
    }
}