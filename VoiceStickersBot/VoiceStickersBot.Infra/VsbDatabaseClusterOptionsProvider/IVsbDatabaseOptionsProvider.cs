namespace VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

public interface IVsbDatabaseOptionsProvider
{
    VsbDatabaseClusterOptions GetOptions();
}