using System.CommandLine;
using System.Globalization;
using Albatross.CommandLine;
using BPOR.Domain.Entities;
using BPOR.Rms.PipelineTrackerIngest.CommandParams;
using BPOR.Rms.PipelineTrackerIngest.SqlWriter;
using CsvHelper;
using CsvHelper.Configuration;

namespace BPOR.Rms.PipelineTrackerIngest.CommandHandlers;

public abstract class IngestHandlerBase<T>(
    ParseResult parseResult,
    T parameters,
    ParticipantDbContext DbContext) : BaseHandler<T>(parseResult, parameters)
    where T : PipelineIngestParamsBase
{
    public sealed override async Task<int> InvokeAsync(CancellationToken cancellationToken)
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

        var allStudies = DbContext.Studies.Where(i => i.IsDeleted == false).ToList();

        using (var output = File.CreateText(parameters.Destination))
        {
            return await InvokeAsync(rows, allStudies, new MySqlSqlWriter(output), cancellationToken);
        }
    }
    
    protected static Study[] MatchStudies(List<Study> existingStudies, PipelineTrackerRow row)
    {
        Study[] candidates;
        candidates = row.CpmsId == null
            ? existingStudies.Where(i => i.IsDeleted == false && NamesMatch(i.StudyName, row.StudyShortName))
                .ToArray()
            : existingStudies.Where(i => i.IsDeleted == false && i.CpmsId == row.CpmsId && NamesMatch(i.StudyName, row.StudyShortName)).ToArray();
        return candidates;
    }

    private static bool NamesMatch(string? first, string? second)
    {
        if (first == null || second == null)
        {
            return false;
        }

        return StringComparer.OrdinalIgnoreCase.Equals(first.Trim(), second.Trim());
    }

    protected abstract Task<int> InvokeAsync(List<PipelineTrackerRow> ingestedRows, List<Study> existingStudies,
        ISqlWriter output, CancellationToken cancellationToken);
}