using Microsoft.EntityFrameworkCore;

namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public class DatabaseTable<TEntity> : DbContext, ITable<TEntity>
    where TEntity : class
{
    public DatabaseTable(DbContextOptions<DatabaseTable<TEntity>> options)
        : base(options)
    {
    }

    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    private DbSet<TEntity> Entities { get; set; } = null!;

    public async Task<TEntity> PerformWriteRequestAsync(TEntity entity)
    {
        var entityEntry = await Entities
            .AddAsync(entity)
            .ConfigureAwait(false);

        await SaveChangesAsync().ConfigureAwait(false);
        return entityEntry.Entity;
    }

    public async Task<List<TEntity>> PerformReadonlyRequestAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> request)
    {
        var entities = await request(Entities)
            .ToListAsync()
            .ConfigureAwait(false);
        await SaveChangesAsync().ConfigureAwait(false);
        return entities;
    }

    public async Task<int> PerformDeletionRequestAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> request)
    {
        var totalDeleted = await request(Entities)
            .ExecuteDeleteAsync()
            .ConfigureAwait(false);
        await SaveChangesAsync().ConfigureAwait(false);
        return totalDeleted;
    }
}