using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using AWS.Lambda.Powertools.Idempotency;
using static AWS.Lambda.Powertools.Idempotency.Idempotency;

namespace BPOR.Tests.Common
{
    public static class AwsIdempotencyExtensions
    {
        public const string IdempotencyTableName = "IdempotencyTable";

        public static IdempotencyBuilder UseInMemoryDb(this IdempotencyBuilder builder)
        {
            builder.WithPersistenceStore(new InMemoryIdempotencyPersistanceStore());
            return builder;
        }
    }
}
