using System.Reflection;
using Ninject;
using Ninject.Extensions.Conventions;

namespace VoiceStickersBot.Infra.VSBApplication;

public abstract class VsbApplicationBase : IVsbApplication
{
    protected readonly StandardKernel container;

    public VsbApplicationBase()
    {
        container = new StandardKernel();
        ConfigureContainer();
    }

    public abstract Task RunAsync(CancellationToken cancellationToken);

    private void ConfigureContainer()
    {
        var currentAssembly = Assembly.GetEntryAssembly();
        container.Bind(c => c
            .From(currentAssembly
                .AsEnumerable()
                .Concat(currentAssembly!
                    .GetReferencedAssemblies()
                    .Select(Assembly.Load)))
            .SelectAllClasses()
            .BindAllInterfaces());

        ConfigureContainer(container);
    }

    protected virtual void ConfigureContainer(StandardKernel containerConfigure)
    {
    }
}