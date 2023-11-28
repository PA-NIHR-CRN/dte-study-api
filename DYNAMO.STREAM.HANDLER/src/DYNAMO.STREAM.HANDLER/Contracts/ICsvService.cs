using DYNAMO.STREAM.HANDLER.Entities;

namespace DYNAMO.STREAM.HANDLER.Contracts;

public interface ICsvService
{
     IAsyncEnumerable<Participant>ReadCsvAsync(CancellationToken cancellationToken);
}
