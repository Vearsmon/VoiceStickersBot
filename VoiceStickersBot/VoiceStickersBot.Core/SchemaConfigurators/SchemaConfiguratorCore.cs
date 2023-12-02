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

    public void Configure()
    {
        var entityTypes = Assembly.GetExecutingAssembly()
            .DefinedTypes
            .Select(type => type.DeclaringType)
            .Where(type => type is not null)
            .Where(type => type!
                .DeclaringType
                ?.CustomAttributes
                .Any(attribute => attribute.AttributeType == typeof(EntityAttribute)) ?? false)
            .ToList();

        var tableCreationMethodInfo =
            typeof(IVsbDatabaseCluster).GetMethod(nameof(IVsbDatabaseCluster.GetSchemaCreator));

        foreach (var entityType in entityTypes)
        {
            var schemaCreator = tableCreationMethodInfo!
                .MakeGenericMethod(entityType!)
                .Invoke(databaseCluster, null) as ISchemaCreator;
            schemaCreator!.EnsureCreated();
        }
    }
}