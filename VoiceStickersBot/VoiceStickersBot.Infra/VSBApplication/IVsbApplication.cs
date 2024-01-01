namespace VoiceStickersBot.Infra.VSBApplication;

public interface IVsbApplication
{
    Task RunAsync(Func<CancellationToken> cancellationTokenGetter);
}