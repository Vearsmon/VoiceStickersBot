using Microsoft.EntityFrameworkCore;
using VoiceStickersBot.Infra.DatabaseTable;
using VoiceStickersBot.Infra.VSBApplication.Log;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public class PostgresVsbDatabaseCluster : IVsbDatabaseCluster
{
    private readonly IVsbDatabaseOptionsProvider vsbDatabaseOptionsProvider;
    private readonly ILog log;

    public PostgresVsbDatabaseCluster(
        IVsbDatabaseOptionsProvider vsbDatabaseOptionsProvider,
        ILog log)
    {
        this.vsbDatabaseOptionsProvider = vsbDatabaseOptionsProvider;
        this.log = log;
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
            .LogTo(log.WriteToLog, LogLevel.Warning)
            .Options;

        return new DatabaseTable<TEntity>(options);
    }
}