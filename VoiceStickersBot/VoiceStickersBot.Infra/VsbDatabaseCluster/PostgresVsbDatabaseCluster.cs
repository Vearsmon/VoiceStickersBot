using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VoiceStickersBot.Infra.DatabaseTable;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public class PostgresVsbDatabaseCluster : IVsbDatabaseCluster
{
    private readonly IVsbDatabaseOptionsProvider vsbDatabaseOptionsProvider;

    public PostgresVsbDatabaseCluster(IVsbDatabaseOptionsProvider vsbDatabaseOptionsProvider)
    {
        this.vsbDatabaseOptionsProvider = vsbDatabaseOptionsProvider;
    }

    public ITable<TEntity> GetTable<TEntity>()
        where TEntity : class
    {
        return CreateDatabaseConnection<TEntity>();
    }

    public ISchemaCreator GetSchemaCreator<TEntity>()
        where TEntity : class
    {
        return CreateDatabaseConnection<TEntity>();
    }

    private DatabaseTable<TEntity> CreateDatabaseConnection<TEntity>()
        where TEntity : class
    {
        var vsbOptions = vsbDatabaseOptionsProvider.GetOptions();

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseTable<TEntity>>();
        var options = optionsBuilder
            .UseNpgsql(vsbOptions.ConnectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .Options;

        return new DatabaseTable<TEntity>(options);
    }
}