namespace VoiceStickersBot.Infra.DatabaseTable;

public interface ITable<TEntity> : IDisposable
    where TEntity : class
{
    Task PerformCreateRequestAsync(
        TEntity entity,
        CancellationToken cancellationToken);

    Task PerformUpdateRequestAsync(
        Func<IQueryable<TEntity>, TEntity> getter,
        Action<TEntity> updater,
        CancellationToken cancellationToken);

    Task<List<TEntity>> PerformReadonlyRequestAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> request,
        CancellationToken cancellationToken);

    Task<List<TExtractedEntity>> PerformReadonlyRequestAsync<TExtractedEntity>(
        Func<IQueryable<TEntity>, IQueryable<TExtractedEntity>> request,
        CancellationToken cancellationToken);

    Task<int> PerformDeletionRequestAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> request,
        CancellationToken cancellationToken);
}