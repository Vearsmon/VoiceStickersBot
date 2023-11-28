namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public interface ITable<TEntity> : IDisposable
    where TEntity : class
{
    Task<TEntity> PerformWriteRequestAsync(TEntity entity);

    public Task<List<TEntity>> PerformReadonlyRequestAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> request);
}