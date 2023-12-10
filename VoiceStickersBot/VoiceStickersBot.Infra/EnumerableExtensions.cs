namespace VoiceStickersBot.Infra;

public static class EnumerableExtensions
{
    public static IEnumerable<TObject> AsEnumerable<TObject>(this TObject tObject)
    {
        yield return tObject;
    }
}