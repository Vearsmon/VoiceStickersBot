namespace VoiceStickersBot.Infra.VSBApplication.Log;

public class FileLog : ILog
{
    private readonly TextWriter writer;
    private readonly object locker;

    public FileLog(TextWriter writer)
    {
        this.writer = writer;

        locker = new object();
    }

    public void WriteToLog(string record)
    {
        lock (locker)
        {
            writer.WriteLine(record);
            writer.Flush();
        }
    }
}