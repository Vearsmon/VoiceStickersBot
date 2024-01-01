using Ninject;
using Ninject.Syntax;
using VoiceStickersBot.Infra.VSBApplication.Log;

namespace VoiceStickersBot.Infra.VSBApplication;

public abstract class VsbApplicationBase : IVsbApplication
{
    private const string AssemblyNamePrefix = "VoiceStickersBot";

    protected readonly IResolutionRoot container;

    public VsbApplicationBase()
    {
        var containerBuilder = new StandardKernel();

        InnerConfigureContainer(containerBuilder);

        container = containerBuilder;
    }

    public async Task RunAsync(Func<CancellationToken> cancellationTokenGetter)
    {
        try
        {
            await RunAsync(cancellationTokenGetter()).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            var log = container.Get<ILog>();
            log.Error(
                e,
                "Application crushed with unknown exception: {0}",
                e.Message);
        }
    }

    protected abstract Task RunAsync(CancellationToken cancellationToken);

    private void InnerConfigureContainer(StandardKernel containerBuilder)
    {
        BindInterfacesWithOnlyImplementation(containerBuilder);

        ConfigureContainer(containerBuilder);
    }

    private static void BindLogger(StandardKernel containerBuilder)
    {
        containerBuilder.Bind<ILog>().To<ConsoleLog>();
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

    protected virtual void ConfigureContainer(StandardKernel containerConfigure)
    {
    }
}