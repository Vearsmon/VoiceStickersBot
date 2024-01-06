using Amazon.S3;
using Amazon.S3.Model;

namespace VoiceStickersBot.Infra.ObjectStorage;

public class ObjectStorageClient : IObjectStorageClient
{
    private static readonly AmazonS3Config objectStorageConfig = new() { ServiceURL = "https://s3.yandexcloud.net" };
    private readonly AmazonS3Client objectStorageClient = new(objectStorageConfig);

    public async Task<MemoryStream> GetObjectFromStorage(ObjectLocation location)
    {
        var objectResponse = await objectStorageClient
            .GetObjectAsync(location.Path, location.FileName)
            .ConfigureAwait(false);
        using var bytes = objectResponse.ResponseStream;
        var byteBuffer = new byte[16 * 1024];
        using var memoryStream = new MemoryStream();
        var bytesRead = 0;
        while ((bytesRead = await bytes.ReadAsync(byteBuffer).ConfigureAwait(false)) > 0)
        {
            await memoryStream.WriteAsync(byteBuffer, 0, bytesRead).ConfigureAwait(false);
        }

        return memoryStream;
    }

    public async Task<ObjectLocation> PutObjectInStorage(string path, Guid objectId, string contentType,
        byte[] objBytes)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = path,
            Key = objectId.ToString(),
            ContentType = contentType
        };

        await using (putRequest.InputStream = new MemoryStream())
        {
            var byteBuffer = new byte[16 * 1024];
            using (var stream = new MemoryStream(objBytes))
            {
                var read = 0;
                while ((read = await stream.ReadAsync(byteBuffer).ConfigureAwait(false)) > 0)
                {
                    await putRequest.InputStream.WriteAsync(byteBuffer, 0, read).ConfigureAwait(false);
                    byteBuffer.Initialize();
                }

                var putResponse = await objectStorageClient.PutObjectAsync(putRequest).ConfigureAwait(false);
                return new ObjectLocation(path, objectId.ToString(), contentType);
            }
        }
    }

    public async Task<ObjectLocation> PutObjectInStorage(
        string path,
        string name,
        string contentType,
        byte[] objBytes)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = path,
            Key = name,
            ContentType = contentType
        };

        await using (putRequest.InputStream = new MemoryStream())
        {
            var byteBuffer = new byte[16 * 1024];
            using (var stream = new MemoryStream(objBytes))
            {
                var read = 0;
                while ((read = await stream.ReadAsync(byteBuffer).ConfigureAwait(false)) > 0)
                {
                    await putRequest.InputStream.WriteAsync(byteBuffer, 0, read).ConfigureAwait(false);
                    byteBuffer.Initialize();
                }

                var putResponse = await objectStorageClient.PutObjectAsync(putRequest).ConfigureAwait(false);
                return new ObjectLocation(path, name, contentType);
            }
        }
    }
}