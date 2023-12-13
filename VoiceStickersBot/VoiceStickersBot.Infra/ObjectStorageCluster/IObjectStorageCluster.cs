using Amazon.S3.Model;

namespace VoiceStickersBot.Infra.ObjectStorageCluster;

public interface IObjectStorageCluster
{
    public byte[] GetObjectFromStorage(S3Bucket bucket, string objectName);

    public PutObjectResponse PutObjectInStorage(S3Bucket bucket, byte[] objBytes, Guid guid);
}