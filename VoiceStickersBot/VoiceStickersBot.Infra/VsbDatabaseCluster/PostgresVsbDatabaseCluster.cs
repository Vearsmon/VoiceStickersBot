using Microsoft.EntityFrameworkCore;
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
        var vsbOptions = vsbDatabaseOptionsProvider.GetOptions();

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseTable<TEntity>>();
        var options = optionsBuilder
            .UseNpgsql(vsbOptions.ConnectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .Options;

        return new DatabaseTable<TEntity>(options);
    }
}