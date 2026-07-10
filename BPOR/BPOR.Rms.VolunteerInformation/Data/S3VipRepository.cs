using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using BPOR.Rms.VolunteerInformation.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BPOR.Rms.VolunteerInformation.Data;

public class S3VipRepository (IMemoryCache memoryCache, IAmazonS3 s3Service, IOptions<VipSettings> options) : VipFileRepository(memoryCache)
{
    protected override async Task<string?> Read(long studyId, CancellationToken cancellationToken)
    {
        try
        {
            var s3object = await s3Service.GetObjectAsync(options.Value.S3BucketName, GetObjectName(studyId), cancellationToken);
            using StreamReader sr = new StreamReader(s3object.ResponseStream);
            return await sr.ReadToEndAsync(cancellationToken);
        }
        catch (AmazonS3Exception caught) when (caught.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

    }

    private string GetObjectName(long studyId) => $"vsi_{studyId}.json";

    protected override async Task CreateOrUpdate(long studyId,
        string content, CancellationToken cancellationToken)
    {
        await s3Service.PutObjectAsync(
            new PutObjectRequest()
            {
                BucketName = options.Value.S3BucketName,
                Key = $"vsi_{studyId}.json",
                ContentBody = content
            }, cancellationToken);
    }
}