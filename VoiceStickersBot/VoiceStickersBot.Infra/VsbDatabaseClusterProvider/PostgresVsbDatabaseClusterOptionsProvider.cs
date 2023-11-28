using Microsoft.Extensions.Configuration;

namespace VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

public class PostgresVsbDatabaseClusterOptionsProvider : IVsbDatabaseClusterOptionsProvider
{
    public VsbDatabaseClusterOptions GetOptions()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory().Split("\\bin")[0]);
        builder.AddJsonFile("VsbDatabaseOptions.json");
        var config = builder.Build();

        var postgresOptions = config
            .GetSection("ClusterOptions")
            .GetSection("PostgresOptions");

        return new VsbDatabaseClusterOptions
        {
            ConnectionString = postgresOptions.GetSection("ConnectionString").Value!
        };
    }
}