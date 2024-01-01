namespace VoiceStickersBot.Infra;

public static class EnumerableExtensions
{
    public static IEnumerable<TObject> AsEnumerable<TObject>(this TObject tObject)
    {
        yield return tObject;
    }

    public static bool IsEmpty<TObject>(this IEnumerable<TObject> enumerable)
    {
        return !enumerable.GetEnumerator().MoveNext();
    }

    public static IEnumerable<TObject> NotNull<TObject>(this IEnumerable<TObject> enumerable)
    {
        return enumerable.Where(o => o is not null);
    }
}