using Amazon.S3;
using Amazon.S3.Model;

namespace VoiceStickersBot.Infra.ObjectStorageCluster;

public class ObjectStorageExtensions
{
    public static List<S3Bucket> GetStorageBuckets(AmazonS3Client client)
    {
        return client.ListBucketsAsync().Result.Buckets.ToList();
    }
    
    public static List<S3Object> GetBucketObjects(AmazonS3Client client, S3Bucket bucket)
    {
        var listRequest = new ListObjectsRequest
        {
            BucketName = bucket.BucketName,
        };
        return client.ListObjectsAsync(listRequest).Result.S3Objects.ToList();
    }
}