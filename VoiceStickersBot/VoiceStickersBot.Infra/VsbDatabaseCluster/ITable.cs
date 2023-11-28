namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public interface ITable<TEntity> : IDisposable
    where TEntity : class
{
    Task<TEntity> PerformWriteRequestAsync(TEntity entity);

    Task<List<TEntity>> PerformReadonlyRequestAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> request);

    Task<int> PerformDeletionRequestAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> request);
}