using System.Reflection;
using Ninject.Infrastructure.Language;

namespace VoiceStickersBot.Infra;

public static class AssemblyHelper
{
    public static IEnumerable<Assembly> GetAssemblies(string assemblyNamePrefix)
    {
        var currentAssembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(
            "Assembly.GetEntryAssembly() is null by unknown reason");

        if (currentAssembly.GetName().Name?.StartsWith(assemblyNamePrefix) ?? false)
            yield return currentAssembly;

        foreach (var name in currentAssembly
                     .GetReferencedAssemblies()
                     .Where(n => n.Name?.StartsWith("VoiceStickersBot") ?? false))
        {
            if (!(name.Name?.StartsWith(assemblyNamePrefix) ?? false))
                continue;

            yield return Assembly.Load(name) ?? throw new InvalidOperationException(
                $"Assembly: {name.Name} is null by unknown reason");
        }
    }

    public static IEnumerable<Type> GetTypesInAssembly(
        this IEnumerable<Assembly> assemblies,
        string assemblyNamePrefix)
    {
        return assemblies.SelectMany(a => a
            .GetTypes()
            .Where(t => t.AssemblyQualifiedName?.StartsWith(assemblyNamePrefix) ?? false));
    }

    public static IEnumerable<Type> GetTypeInheritedFrom(
        this Type type,
        string assemblyNamePrefix)
    {
        return type.GetInterfaces()
            .Concat(type.GetAllBaseTypes())
            .Where(b => b.AssemblyQualifiedName?.StartsWith(assemblyNamePrefix) ?? false);
    }
}