namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public interface ITable<TEntity> : IDisposable
    where TEntity : class
{
    Task PerformCreateRequestAsync(
        TEntity entity,
        CancellationToken cancellationToken);

    Task PerformUpdateRequestAsync(
        TEntity entity,
        CancellationToken cancellationToken);

    Task<List<TEntity>> PerformReadonlyRequestAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> request,
        CancellationToken cancellationToken);

    Task<int> PerformDeletionRequestAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> request,
        CancellationToken cancellationToken);
}