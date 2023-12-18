namespace VoiceStickersBot.Infra.VSBApplication.Log;

public class ConsoleLog : ILog
{
    public void WriteLine(string record)
    {
        Console.WriteLine(record);
    }
}