using System.Globalization;
using System.Runtime.CompilerServices;
using CsvHelper;
using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;

namespace DYNAMO.STREAM.HANDLER.Services;

public class CsvService : ICsvService
{
    public async IAsyncEnumerable<Participant> ReadCsvAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using var reader = new StreamReader("../../../data.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        await foreach (var record in csv.GetRecordsAsync<Participant>(cancellationToken))
        {
            yield return record;
        }
    }
}
