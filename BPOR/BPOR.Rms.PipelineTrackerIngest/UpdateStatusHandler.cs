using System.CommandLine;
using System.Globalization;
using Albatross.CommandLine;
using BPOR.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;

namespace BPOR.Rms.PipelineTrackerIngest;

public class UpdateStatusHandler(ParseResult result, UpdateStatusParams parameters, ParticipantDbContext DbContext)
    : BaseHandler<UpdateStatusParams>(result, parameters) 
{
    public override Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        List<PipelineTrackerRow> rows;
        
        using (var reader = new StreamReader(parameters.Source))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.GetCultureInfo("en-GB"))
               {
                   ShouldSkipRecord = args => args.Row.Parser.Row == 2
               }))
        {
            csv.Context.RegisterClassMap<PipelineTrackerCsvMap>();
            rows = csv.GetRecords<PipelineTrackerRow>().ToList();
        }

        foreach (var row in rows)
        {
            
        }
        
        return Task.FromResult(0);
    }
}