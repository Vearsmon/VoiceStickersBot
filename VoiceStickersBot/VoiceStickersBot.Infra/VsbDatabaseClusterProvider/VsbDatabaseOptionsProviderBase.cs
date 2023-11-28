using Microsoft.Extensions.Configuration;

namespace VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

public abstract class VsbDatabaseOptionsProviderBase : IVsbDatabaseOptionsProvider
{
    public VsbDatabaseClusterOptions GetOptions()
    {
        var config = GetConfig();
        return ExtractOptions(config);
    }

    protected abstract VsbDatabaseClusterOptions ExtractOptions(IConfigurationRoot config);

    private IConfigurationRoot GetConfig()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory().Split("\\bin")[0]);
        builder.AddJsonFile("VsbDatabaseOptions.json");
        return builder.Build();
    }
}