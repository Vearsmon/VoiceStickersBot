using Amazon.S3;
using Amazon.S3.Model;

namespace VoiceStickersBot.Infra.ObjectStorageCluster;

public static class ObjectStorageExtensions
{
    public static async Task<List<S3Bucket>> GetStorageBuckets(AmazonS3Client client)
    {
        return (await client.ListBucketsAsync().ConfigureAwait(false)).Buckets.ToList();
    }
    
    public static async Task<List<S3Object>> GetBucketObjects(AmazonS3Client client, S3Bucket bucket)
    {
        var listRequest = new ListObjectsRequest
        {
            BucketName = bucket.BucketName,
        };
        return (await client.ListObjectsAsync(listRequest).ConfigureAwait(false)).S3Objects.ToList();
    }
}