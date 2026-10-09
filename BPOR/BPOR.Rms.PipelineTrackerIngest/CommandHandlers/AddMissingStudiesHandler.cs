using System.CommandLine;
using BPOR.Domain.Entities;
using BPOR.Rms.PipelineTrackerIngest.SqlWriter;
using Microsoft.Extensions.Logging;

namespace BPOR.Rms.PipelineTrackerIngest;

public class AddMissingStudiesHandler(ParseResult result, UpdateStatusParams parameters, ParticipantDbContext DbContext, ILogger<UpdateStatusHandler> logger)
    : IngestHandlerBase<UpdateStatusParams>(result, parameters, DbContext)
{
    private const int userId = 37;

    protected override Task<int> InvokeAsync(List<PipelineTrackerRow> ingestedRows, List<Study> existingStudies,
        ISqlWriter output, CancellationToken cancellationToken)
    {
        var duplicateCpmsIds = ingestedRows.Where(i => i.CpmsId.HasValue).GroupBy(i => i.CpmsId).Where(g => g.Count() > 1).OrderBy(g => g.Key).ToArray();

        foreach (var duplicateCpmsId in duplicateCpmsIds)
        {
            logger.LogError("CPMS ID {CpmsId} is duplicated in rows {rows}", duplicateCpmsId.Key, string.Join(", ", duplicateCpmsId.Select(i => $"#{i.VsId}|{i.StudyStatus}")));
        }

        int validMatchCount = 0;
        foreach (var row in ingestedRows.Select((row, index) => (row, index)))
        {
            var candidates = MatchStudies(existingStudies, row.row);
            if (candidates.Length == 0)
            {
                WriteInsertStudy(output, row.row, row.index);
                validMatchCount++;
            }
            else if (candidates.Length > 1)
            {
                logger.LogError("{studyMatchCount} studies matched CPMS ID {CpmsId} on row #{rowOrdinal}", candidates.Length, row.row.CpmsId, row.index);
                throw new Exception("Multiple studies matched to row");
            }
        }
        
        logger.LogError("{rowCount} inserts written out of a total of {ingestCount} rows ingested and {studyCount} RMS studies", validMatchCount, ingestedRows.Count, existingStudies.Count);

        return Task.FromResult(0);
    }

    private void WriteInsertStudy(ISqlWriter output, PipelineTrackerRow ingestedRow, int rowIndex)
    {
        output.WriteComment($"VS ID: {ingestedRow.VsId}");

        output.Write(new InsertStatement()
        {
            Target = "Studies", InsertedValues = new
            {
                PipelineVsId = ingestedRow.VsId,
                FullName = ingestedRow.MainContactName,
                EmailAddress = ingestedRow.MainContactEmail,
                StudyName = ingestedRow.StudyShortName,
                CpmsId = ingestedRow.CpmsId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedById = userId,
                UpdatedById = userId,
                StudyStatusId = (int)ingestedRow.StudyStatus,
                RecruitmentStartDate = ingestedRow.RecruitmentStartDate,
                RecruitmentEndDate = ingestedRow.RecruitmentEndDate,
                RecruitmentTarget = ingestedRow.OverallRecruitmentTarget,
                NewApplicationAt = ingestedRow.AddedToRegistryPipelineAt
            }
        });
    }
}