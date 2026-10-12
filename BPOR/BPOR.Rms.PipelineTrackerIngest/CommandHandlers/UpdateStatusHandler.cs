using System.CommandLine;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.PipelineTrackerIngest.CommandParams;
using BPOR.Rms.PipelineTrackerIngest.SqlWriter;
using Microsoft.Extensions.Logging;

namespace BPOR.Rms.PipelineTrackerIngest.CommandHandlers;

public class UpdateStatusHandler(ParseResult result, UpdateStatusParams parameters, ParticipantDbContext DbContext, ILogger<UpdateStatusHandler> logger)
    : IngestHandlerBase<UpdateStatusParams>(result, parameters, DbContext)
{
    private const int userId = 37;

    protected override Task<int> InvokeAsync(List<PipelineTrackerRow> ingestedRows, List<Study> existingStudies,
        ISqlWriter output, CancellationToken cancellationToken)
    {
        List<Study> unusedStudies = new(existingStudies);

        int validMatchCount = 0;
        foreach (var row in ingestedRows.Select((row, index) => (row, index)))
        {
            var candidates = MatchStudies(existingStudies, row.row);
            if (candidates.Length == 1)
            {
                WriteStatusUpdate(output, row.row, row.index, candidates[0]);
                unusedStudies.Remove(candidates[0]);
                validMatchCount++;
            }
            else if (candidates.Length > 1)
            {
                logger.LogError("{studyMatchCount} studies matched CPMS ID {CpmsId} on row #{rowOrdinal}", candidates.Length, row.row.CpmsId, row.index);
                throw new Exception("Multiple studies matched to row");
            }
        }
        
        logger.LogInformation("{rowCount} updates written out of a total of {ingestCount} rows ingested and {studyCount} RMS studies", validMatchCount, ingestedRows.Count, existingStudies.Count);
        foreach (Study unusedStudy in unusedStudies)
        {
            logger.LogWarning("Study #{studyId}, CPMS ID {CpmsId}, Name \"{Name}\" is unmatched", unusedStudy.Id, unusedStudy.CpmsId, unusedStudy.StudyName);
        }
        return Task.FromResult(0);
    }



    private void WriteStatusUpdate(ISqlWriter output, PipelineTrackerRow ingestedRow, int rowIndex, Study study)
    {
        output.WriteComment($"Row {rowIndex}: CPMS ID {study.CpmsId}");

        output.Write(new UpdateStatement
        {
            Table = "Studies",
            UpdatedValues = new
            {
                PipelineVsId = ingestedRow.VsId,
                StudyStatusId = ingestedRow.StudyStatus
            },
            WhereClause = new ComparisonClause
            {
                Lhs = new SqlIdentifier("Id"),
                Op = ComparisonOperator.Equals,
                Rhs = new SqlLiteral(study.Id)
            }
        });
        
        output.Write(new InsertStatement{Table = "StudyStatusHistory", InsertedValues = new
        {
            StudyId = study.Id,
            StudyStatusId = (int)ingestedRow.StudyStatus,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedById = userId,
            UpdatedById = userId,
        }});
        
        output.WriteRawLine("SET @studyStatusHistoryId = LAST_INSERT_ID();");
        
        if (ingestedRow.StudyStatus == StudyStatusType.Rejected)
        {
            output.Write(new InsertStatement{Table = "StudyStatusReasonHistory", InsertedValues = new
            {
                StudyStatusHistoryId = new SqlVariable("@studyStatusHistoryId"),
                RejectedReasonId = ingestedRow.RejectedWithdrawnReasonType.RejectedReason,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedById = userId,
                UpdatedById = userId,
            }});
        }
        else if (ingestedRow.StudyStatus == StudyStatusType.Withdrawn)
        {
            output.Write(new InsertStatement{Table = "StudyStatusReasonHistory", InsertedValues = new
            {
                StudyStatusHistoryId = new SqlVariable("@studyStatusHistoryId"),
                WithdrawnReasonId = ingestedRow.RejectedWithdrawnReasonType.WithdrawnReason,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedById = userId,
                UpdatedById = userId,
            }});
        }
    }
}