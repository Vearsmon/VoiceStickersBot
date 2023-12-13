using Amazon.S3;
using Amazon.S3.Model;

namespace VoiceStickersBot.Infra.ObjectStorageCluster;

public class ObjectStorageCluster : IObjectStorageCluster
{
    public static AmazonS3Config objectStorageConfig = new() { ServiceURL = "https://s3.yandexcloud.net" };
    public AmazonS3Client objectStorageClient = new (objectStorageConfig);

    public byte[] GetObjectFromStorage(S3Bucket bucket, string objectName)
    {
        var objectResponse = objectStorageClient.GetObjectAsync(bucket.BucketName, objectName);
        using var bytes = objectResponse.Result.ResponseStream;
        var byteBuffer = new byte[16*1024];
        using var memoryStream = new MemoryStream();
        int read;
        while ((read = bytes.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
        {
            memoryStream.Write(byteBuffer, 0, read);
        }
        return memoryStream.ToArray();
    }

    public PutObjectResponse PutObjectInStorage(S3Bucket bucket, byte[] objBytes, Guid guid)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = bucket.BucketName,
            Key = guid.ToString(),
            ContentType = "audio/mpeg"
        };
        
        using (putRequest.InputStream = new MemoryStream())
        {
            var byteBuffer = new byte[16*1024];
            int read;
            using (var stream = new MemoryStream(objBytes))
            {
                while ((read = stream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    putRequest.InputStream.Write(byteBuffer, 0, read);
                }
                var putResponse = objectStorageClient.PutObjectAsync(putRequest);
                return putResponse.Result;
            }
        }
    }
}