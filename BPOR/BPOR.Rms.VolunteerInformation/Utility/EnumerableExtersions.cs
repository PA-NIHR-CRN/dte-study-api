namespace BPOR.Rms.VolunteerInformation.Utility;

public static class EnumerableExtensions
{
    public static TValue MaxOrDefault<T, TValue>(this IEnumerable<T> source, Func<T, TValue> valueFunc,
        TValue defaultValue = default) where TValue : IComparable<TValue>
    {
        return source.Select(valueFunc).MaxOrDefault(defaultValue);
    }
    
    
    public static T MaxOrDefault<T>(this IEnumerable<T> source, T defaultValue = default) where T : IComparable<T>
    {
        T result;
        using var enumerator = source.GetEnumerator();
        if (enumerator.MoveNext())
        {
            result = enumerator.Current;
            while (enumerator.MoveNext())
            {
                if (result.CompareTo(enumerator.Current) < 0)
                {
                    result = enumerator.Current;
                }
            }
        }
        else
        {
            result = defaultValue;
        }

        return result;
    }

    public static bool RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        var itemsToRemove = collection.Where(predicate).ToArray();
        foreach (var itemToRemove in itemsToRemove)
        {
            collection.Remove(itemToRemove);
        }

        return itemsToRemove.Length > 0;
    }
}