using System.Collections.Generic;
using System.Text;

namespace NIHR.Infrastructure;

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
    
    public static int CountWords(this string value)
    {
        int result = 0;
        bool inWord = false;
        
        foreach (char c in value)
        {
            bool isWhitespace = char.IsWhiteSpace(c);
            if (!isWhitespace && !inWord)
            {
                result++;
            }
            inWord = !isWhitespace;
        }

        return result;
    }
}