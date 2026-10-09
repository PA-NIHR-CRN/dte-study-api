using System.CommandLine;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.PipelineTrackerIngest.SqlWriter;
using Microsoft.Extensions.Logging;

namespace BPOR.Rms.PipelineTrackerIngest;

public class UpdateStatusHandler(ParseResult result, UpdateStatusParams parameters, ParticipantDbContext DbContext, ILogger<UpdateStatusHandler> logger)
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
            if (candidates.Length == 1)
            {
                WriteStatusUpdate(output, row.row, row.index, candidates[0]);
                validMatchCount++;
            }
            else if (candidates.Length > 1)
            {
                logger.LogError("{studyMatchCount} studies matched CPMS ID {CpmsId} on row #{rowOrdinal}", candidates.Length, row.row.CpmsId, row.index);
                throw new Exception("Multiple studies matched to row");
            }
        }
        
        logger.LogError("{rowCount} updates written out of a total of {ingestCount} rows ingested and {studyCount} RMS studies", validMatchCount, ingestedRows.Count, existingStudies.Count);

        return Task.FromResult(0);
    }



    private void WriteStatusUpdate(ISqlWriter output, PipelineTrackerRow ingestedRow, int rowIndex, Study study)
    {
        var sqlUtcNow = GetSqlDateLiteral(DateTime.UtcNow);

        output.WriteComment($"Row {rowIndex}: CPMS ID {study.CpmsId}");

        output.WriteLine(
            $"UPDATE dte.Studies SET PipelineVsId = {ingestedRow.VsId}, StudyStatusId = {(int)ingestedRow.StudyStatus} WHERE Id = {study.Id};");
        output.Write(new InsertStatement{Target = "StudyStatusHistory", InsertedValues = new
        {
            StudyId = study.Id,
            StudyStatusId = (int)ingestedRow.StudyStatus,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedById = userId,
            UpdatedById = userId,
        }});
        output.WriteLine("SET @studyStatusHistoryId = LAST_INSERT_ID();");
        if (ingestedRow.StudyStatus == StudyStatusType.Rejected)
        {
            var rejectedReason = (int)ingestedRow.RejectedWithdrawnReasonType.RejectedReason;
            output.Write(new InsertStatement{Target = "StudyStatusReasonHistory", InsertedValues = new
            {
                StudyStatusHistoryId = new SqlVariable("@studyStatusHistoryId"),
                RejectedReasonId = rejectedReason,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedById = userId,
                UpdatedById = userId,
            }});
        }
        else if (ingestedRow.StudyStatus == StudyStatusType.Withdrawn)
        {
            var withdrawnReason = (int)ingestedRow.RejectedWithdrawnReasonType.WithdrawnReason;
            output.Write(new InsertStatement{Target = "StudyStatusReasonHistory", InsertedValues = new
            {
                StudyStatusHistoryId = new SqlVariable("@studyStatusHistoryId"),
                WithdrawnReasonId = withdrawnReason,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedById = userId,
                UpdatedById = userId,
            }});
        }
    }

    private string GetSqlDateLiteral(DateTime value)
    {
        return $"\"{value:yyyy-MM-dd}\"";
    }
}