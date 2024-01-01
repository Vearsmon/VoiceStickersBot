namespace VoiceStickersBot.Infra.ObjectStorage;

public class ObjectLocation
{
    public readonly string Path;
    public readonly string FileName;
    public readonly string ContentType;

    public ObjectLocation(string path, string fileName, string contentType)
    {
        Path = path;
        FileName = fileName;
        ContentType = contentType;
    }

    public override string ToString()
    {
        return $@"{Path} {FileName} {ContentType}";
    }

    public static ObjectLocation Parse(string input)
    {
        var data = input.Split();
        if (data.Length != 3)
        {
            throw new ArgumentException(null, nameof(input));
        }

        return new ObjectLocation(data[0], data[1], data[2]);
    }
}