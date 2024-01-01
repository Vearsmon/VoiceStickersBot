namespace VoiceStickersBot.Infra.VSBApplication.Settings;

public class SettingsAttribute : Attribute
{
    public string SettingsName { get; }

    public SettingsAttribute(string settingsName)
    {
        SettingsName = settingsName;
    }
}