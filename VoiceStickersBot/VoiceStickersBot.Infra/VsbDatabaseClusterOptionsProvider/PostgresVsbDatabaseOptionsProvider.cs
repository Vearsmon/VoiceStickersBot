using Microsoft.Extensions.Configuration;

namespace VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

public class PostgresVsbDatabaseOptionsProvider : VsbDatabaseOptionsProviderBase
{
    protected override VsbDatabaseClusterOptions ExtractOptions(IConfigurationRoot config)
    {
        var postgresOptions = config
            .GetSection("ClusterOptions")
            .GetSection("PostgresOptions");

        return new VsbDatabaseClusterOptions
        {
            ConnectionString = postgresOptions.GetSection("ConnectionString").Value!
        };
    }
}