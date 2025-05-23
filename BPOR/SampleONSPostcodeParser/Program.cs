using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Linq;

class Program
{
    const string noInfoText = "No information available";
    static void Process()
    {
        string configFilePath = "config.json";

        try
        {
            Console.WriteLine("Loading configuration...");
            var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(configFilePath));

            if (!CheckRequiredFilesExist(config)) return;

            Console.WriteLine("Loading ICB code mappings...");
            var icbLookup = LoadMappingsStreaming(config.IcbReferenceFilePath, config.IcbLookupCodeColumnNo, config.IcbLookupNameColumnNo);

            Console.WriteLine("Loading LAD code mappings...");
            var ladLookup = LoadMappingsStreaming(config.LadReferenceFilePath, config.LadLookupCodeColumnNo, config.LadLookupNameColumnNo);

            Console.WriteLine("Loading Constituency mappings...");
            var constituencyLookup = LoadMappingsStreaming(config.ConstituencyReferenceFilePath, config.ConstituencyLookupPostcodeColumnNo, config.ConstituencyLookupNameColumnNo);

            // Overwrite check
            if (File.Exists(config.OutputFilePath))
            {
                Console.Write($"Warning: Output file '{config.OutputFilePath}' already exists. Overwrite? (Y/N): ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (char.ToUpper(choice) != 'Y')
                {
                    Console.WriteLine("Operation canceled. Exiting...");
                    return;
                }
            }

            Console.WriteLine("Processing input file...");
            ProcessInputFile(config, icbLookup, ladLookup, constituencyLookup);

            Console.WriteLine($"CSV processing completed. Output saved to {config.OutputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static Boolean CheckRequiredFilesExist(Config config)
    {

        if (!File.Exists(config.NhsPostcodeFilePath))
        {
            Console.WriteLine($"Error: Missing 'NHS Postcode Directory for the UK Full' file.");
            return false;
        }

        if (!File.Exists(config.ConstituencyReferenceFilePath))
        {
            Console.WriteLine($"Error: Missing 'Parliamentary Constituencies Lookup in the UK' file.");
            return false;
        }

        if (!File.Exists(config.IcbReferenceFilePath))
        {
            Console.WriteLine($"Error: Missing 'Integrated Care Boards Names and Codes in EN' file.");
            return false;
        }

        if (!File.Exists(config.IcbReferenceFilePath))
        {
            Console.WriteLine($"Error: Missing 'Local Authority Districts Names and Codes in the UK' file.");
            return false;
        }

        return true;
    }

    static Dictionary<string, string> LoadMappingsStreaming(string filePath, int codeColumnNo, int nameColumnNo)
    {
        var lookup = new Dictionary<string, string>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: Reference file '{filePath}' not found.");
            return lookup;
        }

        using var reader = new StreamReader(filePath);
        reader.ReadLine(); // Skip header row

        while (!reader.EndOfStream)
        {
            var parts = ParseCsvLine(reader.ReadLine());
            if (parts.Length > Math.Max(codeColumnNo, nameColumnNo))
            {
                lookup[parts[codeColumnNo].Trim()] = parts[nameColumnNo].Trim();
            }
        }

        return lookup;
    }

    static void ProcessInputFile(Config config, Dictionary<string, string> icbLookup, Dictionary<string, string> ladLookup, Dictionary<string, string> constituencyLookup)
    {
        using var reader = new StreamReader(config.NhsPostcodeFilePath);
        using var writer = new StreamWriter(config.OutputFilePath);

        int processedRows = 0, icbFallbackCount = 0, ladFallbackCount = 0, constituencyNotFoundCount = 0;

        while (!reader.EndOfStream)
        {
            if (config.DebugMode && processedRows >= config.DebugRowLimit)
            {
                Console.WriteLine($"\nDebug Mode active: Stopped after {config.DebugRowLimit} rows.");
                break;
            }

            string line = reader.ReadLine();
            string processedLine = ExtractColumns(line, config, icbLookup, ladLookup, constituencyLookup, ref icbFallbackCount, ref ladFallbackCount, ref constituencyNotFoundCount);
            writer.WriteLine(processedLine);

            processedRows++;
            Console.Write($"\rProcessing row {processedRows} (ICB not found: {icbFallbackCount}, LAD not found: {ladFallbackCount}, Constituency not found: {constituencyNotFoundCount})...");
        }

        Console.WriteLine("\nProcessing completed.");
        Console.WriteLine($"Total rows processed: {processedRows}");
        Console.WriteLine($"ICB not found count: {icbFallbackCount}");
        Console.WriteLine($"LAD not found count: {ladFallbackCount}");
        Console.WriteLine($"Constituency not found count: {constituencyNotFoundCount}");
    }

    static string ExtractColumns(string line, Config config, Dictionary<string, string> icbLookup, Dictionary<string, string> ladLookup, Dictionary<string, string> constituencyLookup,
                                 ref int icbFallbackCount, ref int ladFallbackCount, ref int constituencyNotFoundCount)
    {
        var values = ParseCsvLine(line);

        string postcode = values.Length > config.PostcodeColumnNo ? values[config.PostcodeColumnNo] : "";
        string standardisedPostcode = values.Length > config.StandardisedPostcodeColumnNo ? values[config.StandardisedPostcodeColumnNo] : "";

        // Extract ICB values with fallback
        string icbCode = values.Length > config.IcbCodeColumnNo ? values[config.IcbCodeColumnNo] : "";
        string icbName = icbLookup.ContainsKey(icbCode)
            ? icbLookup[icbCode]
            : config.FallbackIcbCodes.ContainsKey(icbCode) ? config.FallbackIcbCodes[icbCode] : noInfoText;
        if (icbName == noInfoText) icbFallbackCount++;

        // Extract LAD values with fallback
        string ladCode = values.Length > config.LadCodeColumnNo ? values[config.LadCodeColumnNo] : "";
        string ladName = ladLookup.ContainsKey(ladCode)
            ? ladLookup[ladCode]
            : config.FallbackLadCodes.ContainsKey(ladCode) ? config.FallbackLadCodes[ladCode] : noInfoText;
        if (ladName == noInfoText) ladFallbackCount++;

        //constituency postcodes 7 char max in csv and strips the middle space
        // so bt11 1ab becomes bt111ab
        // but bt2 2ab would remain the same
        string constituencyPostCode = standardisedPostcode.Length <= 7 ? standardisedPostcode : standardisedPostcode.Replace(" ", "");
        string constituencyName = constituencyLookup.ContainsKey(standardisedPostcode)
            ? constituencyLookup[standardisedPostcode]
            : noInfoText;

        if (!constituencyLookup.ContainsKey(constituencyPostCode))
            constituencyNotFoundCount++;

        return string.Join(",", new[]
        {
            $"\"{postcode}\"",
            $"\"{standardisedPostcode}\"",
            //$"\"{icbCode}\"",
            $"\"{icbName}\"",
            //$"\"{ladCode}\"",
            $"\"{ladName}\"",
            $"\"{constituencyName}\""
        });
    }

    static void Main()
    {
        Process();
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static string[] ParseCsvLine(string line)
    {
        var matches = Regex.Matches(line, "(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)");
        return matches.Cast<Match>().Select(m => m.Value.TrimStart(',').Trim('"').Replace("\"\"", "\"")).ToArray();
    }
}


class Config
{
    public string NhsPostcodeFilePath { get; set; }
    public string OutputFilePath { get; set; }
    public int PostcodeColumnNo { get; set; }
    public int StandardisedPostcodeColumnNo { get; set; }
    public int IcbCodeColumnNo { get; set; }
    public int LadCodeColumnNo { get; set; }
    public string IcbReferenceFilePath { get; set; }
    public int IcbLookupCodeColumnNo { get; set; }
    public int IcbLookupNameColumnNo { get; set; }
    public string LadReferenceFilePath { get; set; }
    public int LadLookupCodeColumnNo { get; set; }
    public int LadLookupNameColumnNo { get; set; }
    public string ConstituencyReferenceFilePath { get; set; }
    public int ConstituencyLookupPostcodeColumnNo { get; set; }
    public int ConstituencyLookupNameColumnNo { get; set; }
    public Dictionary<string, string> FallbackIcbCodes { get; set; }
    public Dictionary<string, string> FallbackLadCodes { get; set; }
    public bool DebugMode { get; set; }
    public int DebugRowLimit { get; set; }
}
