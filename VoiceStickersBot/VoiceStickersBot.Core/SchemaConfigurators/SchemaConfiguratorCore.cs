using System.Reflection;
using VoiceStickersBot.Infra.DatabaseTable;
using VoiceStickersBot.Infra.VsbDatabaseCluster;

namespace VoiceStickersBot.Core.SchemaConfigurators;

public class SchemaConfiguratorCore
{
    private readonly IVsbDatabaseCluster databaseCluster;
    private readonly MethodInfo tableCreationMethodInfo;

    public SchemaConfiguratorCore(IVsbDatabaseCluster databaseCluster)
    {
        this.databaseCluster = databaseCluster;

        tableCreationMethodInfo = typeof(IVsbDatabaseCluster)
            .GetMethod(nameof(IVsbDatabaseCluster.GetSchemaCreator))!;
    }

    public async Task ConfigureAsync()
    {
        foreach (var entityType in GetEntityTypes())
        {
            using var schemaCreator = tableCreationMethodInfo
                .MakeGenericMethod(entityType)
                .Invoke(databaseCluster, null) as ISchemaCreator;
            await schemaCreator!.EnsureCreatedAsync().ConfigureAwait(false);
        }
    }

    private static IEnumerable<TypeInfo> GetEntityTypes()
    {
        return Assembly
            .GetExecutingAssembly()
            .DefinedTypes
            .Where(type => type.GetCustomAttribute<EntityAttribute>() is not null);
    }
}