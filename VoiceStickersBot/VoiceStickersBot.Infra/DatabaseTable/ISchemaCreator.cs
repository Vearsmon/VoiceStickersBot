namespace VoiceStickersBot.Infra.DatabaseTable;

public interface ISchemaCreator : IDisposable
{
    Task<bool> EnsureCreatedAsync();
}