namespace VoiceStickersBot.Infra.VSBApplication.Log;

public class FileLog : ILog
{
    private readonly TextWriter writer;

    public FileLog(TextWriter writer)
    {
        this.writer = writer;
    }

    public void WriteToLog(string record)
    {
        writer.WriteLine(record);
    }
}