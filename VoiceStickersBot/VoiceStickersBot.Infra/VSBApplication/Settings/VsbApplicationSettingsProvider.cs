using System.Text;
using System.Text.Json;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.Infra.VSBApplication.Settings;

public class VsbApplicationSettingsProvider : IVsbApplicationSettingsProvider
{
    private readonly ObjectStorageClient objectStorageClient;

    public VsbApplicationSettingsProvider(ObjectStorageClient objectStorageClient)
    {
        this.objectStorageClient = objectStorageClient;
    }

    public async Task<VsbApplicationSettings> GetAsync(string name)
    {
        var settingsLocation = new ObjectLocation("application-settings", name, "application/json");
        var settingsBytes = await objectStorageClient.GetObjectFromStorage(settingsLocation).ConfigureAwait(false);
        var settingsString = Encoding.Default.GetString(settingsBytes);
        var settings = JsonSerializer.Deserialize<Dictionary<string, string>>(settingsString);

        if (settings is null) throw new ArgumentException($"Unable to parse settings: \n\t {settings}");

        return new VsbApplicationSettings
        {
            Settings = settings
        };
    }
}