namespace VoiceStickersBot.Infra.DatabaseTable;

public interface ISchemaCreator
{
    bool EnsureCreated();
}