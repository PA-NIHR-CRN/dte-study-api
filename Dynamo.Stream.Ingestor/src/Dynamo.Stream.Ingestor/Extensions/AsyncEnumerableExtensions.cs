namespace Dynamo.Stream.Ingestor.Extensions;

public static class AsyncEnumerableExtensions
{
    public static async IAsyncEnumerable<IEnumerable<T>> Batch<T>(this IAsyncEnumerable<T> source, int size)
    {
        List<T> batch = new List<T>(size);
        await foreach (var item in source)
        {
            batch.Add(item);
            if (batch.Count == size)
            {
                yield return batch;
                batch.Clear();
            }
        }

        if (batch.Any())
        {
            yield return batch;
        }
    }
}

