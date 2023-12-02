using VoiceStickersBot.Infra.DatabaseTable;

namespace VoiceStickersBot.Infra.VsbDatabaseCluster;

public interface IVsbDatabaseCluster
{
    public ITable<TEntity> GetTable<TEntity>()
        where TEntity : class;

    public ISchemaCreator GetSchemaCreator<TEntity>()
        where TEntity : class;
}