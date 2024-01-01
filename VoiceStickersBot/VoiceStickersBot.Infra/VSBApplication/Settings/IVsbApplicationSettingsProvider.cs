namespace VoiceStickersBot.Infra.VSBApplication.Settings;

public interface IVsbApplicationSettingsProvider
{
    Task<VsbApplicationSettings> GetAsync(string name);
}