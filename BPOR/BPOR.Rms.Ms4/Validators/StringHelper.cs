using System.Text;

namespace BPOR.Rms.Ms4.Validators;

public static class StringHelper
{
    public static string Join(string separator, string finalSeparator, IEnumerable<string> values)
    {
        var enumerator = values.GetEnumerator();
        StringBuilder result = new StringBuilder();
        if (enumerator.MoveNext())
        {
            result.Append(enumerator.Current);
            bool isWorking = enumerator.MoveNext();
            while (isWorking)
            {
                string current = enumerator.Current;
                isWorking =  enumerator.MoveNext();
                result.Append(isWorking ? separator : finalSeparator);
                result.Append(current);
            }
        }
        return result.ToString();
    }
}