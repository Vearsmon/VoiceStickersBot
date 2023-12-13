using Amazon.S3.Model;

namespace VoiceStickersBot.Infra.ObjectStorageCluster;

public interface IObjectStorageClient
{
    public Task<byte[]> GetObjectFromStorage(ObjectLocation location);

    public Task<ObjectLocation> PutObjectInStorage(string path, Guid objectId, string contentType, byte[] objBytes);
}