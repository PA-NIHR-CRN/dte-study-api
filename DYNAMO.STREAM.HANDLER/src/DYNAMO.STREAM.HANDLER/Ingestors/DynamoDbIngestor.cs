using DYNAMO.STREAM.HANDLER.Contracts;

namespace DYNAMO.STREAM.HANDLER.Ingestors;

public class DynamoDbIngestor : IDataIngestor
{
    public Task IngestDataAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
