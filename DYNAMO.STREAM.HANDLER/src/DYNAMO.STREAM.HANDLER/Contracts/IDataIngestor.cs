namespace DYNAMO.STREAM.HANDLER.Contracts;

public interface IDataIngestor
{
    Task IngestDataAsync(CancellationToken cancellationToken);
}
