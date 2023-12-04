using System.Reflection;
using VoiceStickersBot.Infra.DatabaseTable;
using VoiceStickersBot.Infra.VsbDatabaseCluster;

namespace VoiceStickersBot.Core.SchemaConfigurators;

public class SchemaConfiguratorCore
{
    private readonly IVsbDatabaseCluster databaseCluster;

    public SchemaConfiguratorCore(IVsbDatabaseCluster databaseCluster)
    {
        this.databaseCluster = databaseCluster;
    }

    public async void ConfigureAsync()
    {
        var entityTypes = Assembly.GetExecutingAssembly()
            .DefinedTypes
            .Where(type => type.GetCustomAttribute<EntityAttribute>() is not null)
            .ToList();

        var tableCreationMethodInfo =
            typeof(IVsbDatabaseCluster).GetMethod(nameof(IVsbDatabaseCluster.GetSchemaCreator));

        foreach (var entityType in entityTypes)
        {
            using var schemaCreator = tableCreationMethodInfo!
                .MakeGenericMethod(entityType!)
                .Invoke(databaseCluster, null) as ISchemaCreator;
            await schemaCreator!.EnsureCreatedAsync().ConfigureAwait(false);
        }
    }
}