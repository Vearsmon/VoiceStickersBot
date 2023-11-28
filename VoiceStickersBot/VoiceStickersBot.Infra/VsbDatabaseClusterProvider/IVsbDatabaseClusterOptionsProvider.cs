namespace VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

public interface IVsbDatabaseClusterOptionsProvider
{
    VsbDatabaseClusterOptions GetOptions();
}