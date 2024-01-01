using System.Reflection;
using VoiceStickersBot.Infra;

var currentAssembly = Assembly.GetEntryAssembly();

var typesToTheirDerivedTypes = currentAssembly
    .AsEnumerable()
    .Concat(currentAssembly!
        .GetReferencedAssemblies()
        .Where(t => t.Name?.StartsWith("VoiceStickersBot") ?? false)
        .Select(Assembly.Load))
    .NotNull();
foreach (var n in typesToTheirDerivedTypes.Select(t => t!.FullName))
    Console.WriteLine(n);