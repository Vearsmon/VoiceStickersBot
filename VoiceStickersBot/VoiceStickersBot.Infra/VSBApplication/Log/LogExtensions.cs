using System.Text.RegularExpressions;

namespace VoiceStickersBot.Infra.VSBApplication.Log;

public static partial class LogExtensions
{
    public static void Error(
        this ILog log,
        string messageTemplate,
        params object[] arguments)
    {
        log.Log(LogLevel.Error, null, messageTemplate, arguments);
    }

    public static void Error(
        this ILog log,
        Exception? exception,
        string messageTemplate,
        params object[] arguments)
    {
        log.Log(LogLevel.Error, exception, messageTemplate, arguments);
    }

    public static void Warn(
        this ILog log,
        string messageTemplate,
        params object[] arguments)
    {
        log.Log(LogLevel.Warn, null, messageTemplate, arguments);
    }

    public static void Warn(
        this ILog log,
        Exception? exception,
        string messageTemplate,
        params object[] arguments)
    {
        log.Log(LogLevel.Warn, exception, messageTemplate, arguments);
    }

    public static void Info(
        this ILog log,
        string messageTemplate,
        params object[] arguments)
    {
        log.Log(LogLevel.Info, null, messageTemplate, arguments);
    }

    public static void Info(
        this ILog log,
        Exception? exception,
        string messageTemplate,
        params object[] arguments)
    {
        log.Log(LogLevel.Info, exception, messageTemplate, arguments);
    }

    public static void Debug(
        this ILog log,
        string messageTemplate,
        params object[] arguments)
    {
        log.Log(LogLevel.Debug, null, messageTemplate, arguments);
    }

    public static void Debug(
        this ILog log,
        Exception? exception,
        string messageTemplate,
        params object[] arguments)
    {
        log.Log(LogLevel.Debug, exception, messageTemplate, arguments);
    }

    public static void Log(
        this ILog log,
        LogLevel level,
        Exception? exception,
        string messageTemplate,
        params object[] arguments)
    {
        ValidateArgumentsCount(messageTemplate, arguments.Length);

        var logRecord = ArgumentsRegex().Replace(messageTemplate, match => Replace(match, arguments));
        log.WriteLine($"{level} {DateTime.Now}: {logRecord}");

        if (exception != null) log.WriteLine(exception.ToString());
    }

    private static string Replace(Match match, object[] arguments)
    {
        var argumentNumber = int.Parse(match.Value[1..^1]);
        return arguments[argumentNumber].ToString() ?? string.Empty;
    }

    private static void ValidateArgumentsCount(string messageTemplate, int argumentsCount)
    {
        var actualArgumentsCount = ArgumentsRegex()
            .Matches(messageTemplate)
            .DistinctBy(m => m.Value)
            .Count();
        if (actualArgumentsCount != argumentsCount)
            throw new ArgumentException(
                $"Invalid count of parameters [{actualArgumentsCount}] for [{messageTemplate}]");
    }

    [GeneratedRegex("{\\d}")]
    private static partial Regex ArgumentsRegex();
}