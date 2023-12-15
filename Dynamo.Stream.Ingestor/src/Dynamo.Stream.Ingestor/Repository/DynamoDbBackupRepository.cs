using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dynamo.Stream.Ingestor.Repository
{
    public class DynamoDbBackupRepository : IDynamoParticipantRepository

    {
        private readonly ILogger<DynamoDbBackupRepository> _logger;
        private readonly DynamoDbBackupSettings _settings;
        IEnumerable<ManifestFile?> files;

        public DynamoDbBackupRepository(ILogger<DynamoDbBackupRepository> logger, IOptions<DynamoDbBackupSettings> settings)
        {
            _settings = settings.Value;

            var summary = JsonSerializer.Deserialize<ManifestSummary>(File.ReadAllText(Path.Combine(_settings.RootPath, _settings.ExportName, "manifest-summary.json")));

            _logger.LogInformation("Reading backup {exportArn}. Item Count: {exportItemCount}", summary.ExportArn, summary.ItemCount);

            var filesPath = Path.GetFileName(summary.ManifestFilesS3Key);

            files = File.ReadLines(Path.Combine(_settings.RootPath, summary.ManifestFilesS3Key))
                .Select(x => JsonSerializer.Deserialize<ManifestFile>(x));
            _logger = logger;
        }

        public async IAsyncEnumerable<Dictionary<string, AttributeValue>> GetAllParticipantsAsAttributeMapsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            foreach (var f in files)
            {
                _logger.LogInformation("Reading data file {dataFileS3Key}. Item count {dataFileItemCount}", f.DataFileS3Key, f.ItemCount);

                using var ss = new FileStream(Path.Combine(_settings.RootPath, f.DataFileS3Key), FileMode.Open);

                using var ms = new MemoryStream();

                using (var gZipStream = new GZipStream(ss, CompressionMode.Decompress))
                {
                    gZipStream.CopyTo(ms);
                }

                ms.Seek(0, SeekOrigin.Begin);

                var sr = new StreamReader(ms, Encoding.UTF8);

                cancellationToken.ThrowIfCancellationRequested();
                var item = await sr.ReadLineAsync();

                while (item != null)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var document = Amazon.DynamoDBv2.DocumentModel.Document.FromJson(item);
                    yield return document.ToAttributeMap();
                }
            }
        }
    }

    public class ManifestFile
    {
        [JsonPropertyName("itemCount")]
        public int ItemCount { get; set; }

        [JsonPropertyName("md5Checksum")]
        public string Md5Checksum { get; set; }

        [JsonPropertyName("etag")]
        public string Etag { get; set; }

        [JsonPropertyName("dataFileS3Key")]
        public string DataFileS3Key { get; set; }
    }

    public class ManifestSummary
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("exportArn")]
        public string ExportArn { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("tableArn")]
        public string TableArn { get; set; }

        [JsonPropertyName("tableId")]
        public string TableId { get; set; }

        [JsonPropertyName("exportTime")]
        public DateTime ExportTime { get; set; }

        [JsonPropertyName("s3Bucket")]
        public string S3Bucket { get; set; }

        [JsonPropertyName("s3Prefix")]
        public object S3Prefix { get; set; }

        [JsonPropertyName("s3SseAlgorithm")]
        public string S3SseAlgorithm { get; set; }

        [JsonPropertyName("s3SseKmsKeyId")]
        public object S3SseKmsKeyId { get; set; }

        [JsonPropertyName("manifestFilesS3Key")]
        public string ManifestFilesS3Key { get; set; }

        [JsonPropertyName("billedSizeBytes")]
        public int BilledSizeBytes { get; set; }

        [JsonPropertyName("itemCount")]
        public int ItemCount { get; set; }

        [JsonPropertyName("outputFormat")]
        public string OutputFormat { get; set; }
    }
}
