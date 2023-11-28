using Microsoft.EntityFrameworkCore;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public class PostgresVsbDatabaseCluster : IVsbDatabaseCluster
{
    private readonly IVsbDatabaseClusterOptionsProvider optionsProvider;

    public PostgresVsbDatabaseCluster(IVsbDatabaseClusterOptionsProvider optionsProvider)
    {
        this.optionsProvider = optionsProvider;
    }

    public ITable<TEntity> GetTable<TEntity>()
        where TEntity : class
    {
        var vsbOptions = optionsProvider.GetOptions();

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseTable<TEntity>>();
        var options = optionsBuilder
            .UseNpgsql(vsbOptions.ConnectionString)
            .Options;

        return new DatabaseTable<TEntity>(options);
    }
}