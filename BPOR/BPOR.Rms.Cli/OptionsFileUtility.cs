using System.Text.Json;

namespace BPOR.Rms.Cli;

public static class OptionsFileUtility
{
    public static T LoadOptionsFromFile<T>(string? filename)
        where T : new()
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            return new T();
        }
        
        try
        {
            using var file = File.OpenRead(filename);
            return JsonSerializer.Deserialize<T>(file)!;
        }
        catch (FileNotFoundException)
        {
            return new T();
        }
    }
}