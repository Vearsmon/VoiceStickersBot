using System.Reflection;
using Ninject;
using Ninject.Syntax;
using VoiceStickersBot.Infra.ObjectStorage;
using VoiceStickersBot.Infra.VSBApplication.Log;
using VoiceStickersBot.Infra.VSBApplication.Settings;

namespace VoiceStickersBot.Infra.VSBApplication;

public abstract class VsbApplicationBase : IVsbApplication
{
    private const string AssemblyNamePrefix = "VoiceStickersBot";

    protected readonly IResolutionRoot Container;
    protected readonly VsbApplicationSettings ApplicationSettings;

    protected VsbApplicationBase()
    {
        ApplicationSettings = GetSettings();

        var containerBuilder = new StandardKernel();

        InnerConfigureContainer(containerBuilder);
        Container = containerBuilder;
    }

    public async Task RunAsync(Func<CancellationToken> cancellationTokenGetter)
    {
        try
        {
            await RunAsync(cancellationTokenGetter()).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            var log = Container.Get<ILog>();
            log.Error(
                e,
                "Application crushed with unknown exception: {0}",
                e.Message);
        }
    }

    protected abstract Task RunAsync(CancellationToken cancellationToken);

    protected virtual void ConfigureContainer(StandardKernel containerBuilder)
    {
    }

    private void InnerConfigureContainer(StandardKernel containerBuilder)
    {
        BindInterfacesWithOnlyImplementation(containerBuilder);
        BindLogger(containerBuilder);
        ConfigureContainer(containerBuilder);
    }

    private static void BindLogger(StandardKernel containerBuilder)
    {
        var dateTime = DateTime.Now;
        containerBuilder
            .Bind<ILog>()
            .ToConstant(new FileLog(File.CreateText($"log_{dateTime.Hour}_{dateTime.Minute}.txt")));
    }

    private static void BindInterfacesWithOnlyImplementation(StandardKernel containerBuilder)
    {
        var baseToTypes = AssemblyHelper
            .GetAssemblies(AssemblyNamePrefix)
            .GetTypesInAssembly(AssemblyNamePrefix)
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .SelectMany(t => t
                .GetTypeInheritedFrom(AssemblyNamePrefix)
                .Select(b => (b, t)))
            .GroupBy(tuple => tuple.b, tuple => tuple.t);

        foreach (var group in baseToTypes)
        {
            var derived = group.ToList();
            if (derived.Count != 1)
                continue;

            containerBuilder.Bind(group.Key).To(derived.Single()).InSingletonScope();
        }
    }

    private VsbApplicationSettings GetSettings()
    {
        var settingsName = GetType().GetCustomAttribute<SettingsAttribute>()?.SettingsName;
        if (settingsName is null)
            return new VsbApplicationSettings
            {
                Settings = new Dictionary<string, string>()
            };

        var settingsProvider = new VsbApplicationSettingsProvider(new ObjectStorageClient());

        return settingsProvider.GetAsync(settingsName).GetAwaiter().GetResult();
    }
}