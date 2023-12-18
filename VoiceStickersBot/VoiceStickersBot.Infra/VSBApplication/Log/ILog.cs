namespace VoiceStickersBot.Infra.VSBApplication.Log;

public partial interface ILog
{
    public void WriteLine(string record);
}