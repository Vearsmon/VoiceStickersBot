using Microsoft.EntityFrameworkCore;

namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public class DatabaseTable<TEntity> : DbContext, ITable<TEntity>
    where TEntity : class
{
    public DatabaseTable(DbContextOptions<DatabaseTable<TEntity>> options)
        : base(options)
    {
    }

    private DbSet<TEntity> Entities { get; } = null!;

    public async Task<TEntity> PerformWriteRequestAsync(TEntity entity)
    {
        var entityWritten = (await Entities.AddAsync(entity).ConfigureAwait(false)).Entity;
        await SaveChangesAsync();
        return entityWritten;
    }

    public async Task<List<TEntity>> PerformReadonlyRequestAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> request)
    {
        var entities = await request(Entities)
            .ToListAsync()
            .ConfigureAwait(false);
        await SaveChangesAsync();
        return entities;
    }
}