namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public interface IVsbDatabaseCluster
{
    public ITable<TEntity> GetTable<TEntity>()
        where TEntity : class;
}