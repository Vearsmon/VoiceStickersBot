namespace VoiceStickersBot.Infra.VSBApplication;

public interface IVsbApplication
{
    Task RunAsync(CancellationToken cancellationToken);
}