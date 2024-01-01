namespace VoiceStickersBot.Infra.VSBApplication.Log;

public class ConsoleLog : ILog
{
    public void WriteToLog(string record)
    {
        Console.WriteLine(record);
    }
}