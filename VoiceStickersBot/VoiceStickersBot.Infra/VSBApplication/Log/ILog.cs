namespace VoiceStickersBot.Infra.VSBApplication.Log;

public partial interface ILog
{
    public void WriteToLog(string record);
}