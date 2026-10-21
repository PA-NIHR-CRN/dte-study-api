using System.CommandLine;
using System.Globalization;
using Albatross.CommandLine;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NIHR.SqlWriter;

namespace BPOR.Rms.Cli.CreateIngestScripts;

public class CreateIngestScriptsHandler(
    ParseResult parseResult,
    CreateIngestScriptsParams parameters,
    ParticipantDbContext dbContext,
    ILogger<CreateIngestScriptsHandler> logger) : BaseHandler<CreateIngestScriptsParams>(parseResult, parameters)
{
    private CreateIngestScriptOptions _options;

    public sealed override async Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        _options = OptionsFileUtility.LoadOptionsFromFile<CreateIngestScriptOptions>(parameters.OptionsFileName);
        
        List<PipelineTrackerRow> rows;

        using (var reader = new StreamReader(parameters.Source))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.GetCultureInfo("en-GB"))
               {
                   ShouldSkipRecord = args => args.Row.Parser.Row == 2
               }))
        {
            csv.Context.RegisterClassMap<PipelineTrackerCsvMap>();
            rows = csv.GetRecords<PipelineTrackerRow>()
                .Where(row => !_options.SkipVsIds.Contains(row.VsId)).ToList();
        }
        

        var allStudies = dbContext.Studies.Include(i => i.StudyStatusHistories)
            .ThenInclude(i => i.StudyStatusReasonHistories).Where(i => i.IsDeleted == false).ToList();

        var utcNow = DateTime.UtcNow;
        
        using (var output = File.CreateText(Path.Join(parameters.Destination ?? ".", $"{utcNow:yyyy-MM-ddTHH_mm_ss}-update-status.sql")))
        {
            CreateStatusUpdateScript(rows, allStudies, new MySqlSqlWriter(output), parameters.UserId);
        }
        
        using (var output = File.CreateText(Path.Join(parameters.Destination ?? ".", $"{utcNow:yyyy-MM-ddTHH_mm_ss}-add-studies.sql")))
        {
            CreateAddMissingStudiesScript(rows, allStudies, new MySqlSqlWriter(output), parameters.UserId);
        }

        return 1;
    }
    
    protected static Study[] MatchStudies(List<Study> existingStudies, PipelineTrackerRow row)
    {
        Study[] candidates;
        if (row.CpmsId == null)
        {
            candidates = existingStudies.Where(i => i.IsDeleted == false && NamesMatch(i.StudyName, row.StudyShortName))
                .ToArray();
        }
        else
        {
            candidates = existingStudies.Where(i => i.IsDeleted == false && i.CpmsId == row.CpmsId).ToArray();
            candidates = candidates.Where(i => NamesMatch(i.StudyName, row.StudyShortName)).ToArray();
        }
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
    
    private void CreateStatusUpdateScript(List<PipelineTrackerRow> ingestedRows, List<Study> existingStudies,
        ISqlWriter output, int userId)
    {
        List<Study> unusedStudies = new(existingStudies);
        Dictionary<int, List<int>> mappedStudyIds = new();
        
        int validMatchCount = 0;
        foreach (var row in ingestedRows.Select((row, index) => (row, index)))
        {

            
            var candidates = MatchStudies(existingStudies, row.row);
            if (candidates.Length == 1)
            {
                if (!mappedStudyIds.TryGetValue(candidates[0].Id, out var vsIdList))
                {
                    vsIdList = new();
                    mappedStudyIds.Add(candidates[0].Id, vsIdList);
                }
                
                vsIdList.Add(row.row.VsId);
                WriteStatusUpdate(output, row.row, candidates[0], userId);
                unusedStudies.Remove(candidates[0]);
                validMatchCount++;
            }
            else if (candidates.Length > 1)
            {
                logger.LogError("{studyMatchCount} studies matched CPMS ID {CpmsId} on row #{rowOrdinal}",
                    candidates.Length, row.row.CpmsId, row.index);
                throw new Exception("Multiple studies matched to row");
            }
        }

        foreach (var vsIdList in mappedStudyIds.Values.Where(i => i.Count > 1))
        {
            logger.LogError("Duplicate status mapping found for VS IDs {VsIds}", string.Join(", ", vsIdList));
        }

        logger.LogInformation(
            "{rowCount} updates written out of a total of {ingestCount} rows ingested and {studyCount} RMS studies",
            validMatchCount, ingestedRows.Count, existingStudies.Count);
        foreach (Study unusedStudy in unusedStudies)
        {
            logger.LogWarning("Study #{studyId}, CPMS ID {CpmsId}, Name \"{Name}\" is unmatched", unusedStudy.Id,
                unusedStudy.CpmsId, unusedStudy.StudyName);
        }
    }


    private void WriteStatusUpdate(ISqlWriter output, PipelineTrackerRow ingestedRow, Study study, int userId)
    {
        output.WriteComment($"VS ID {ingestedRow.VsId}: CPMS ID {study.CpmsId}");

        if (StatusDiffers(study, ingestedRow))
        {
            output.Update("Studies", new
            {
                PipelineVsId = ingestedRow.VsId,
                StudyStatusId = ingestedRow.StudyStatus
            }, new ComparisonExpr
            {
                Lhs = new SqlIdentifier("Id"),
                Op = ComparisonOperator.Equals,
                Rhs = new SqlLiteral(study.Id)
            });

            var ingestedRowRejectedWithdrawnReasonType = ingestedRow.RejectedWithdrawnReasonType;

            var ingestedRowStudyStatus = ingestedRow.StudyStatus;

            WriteInsertStatusHistory(output, new SqlLiteral(study.Id), ingestedRowStudyStatus,
                ingestedRowRejectedWithdrawnReasonType, userId);
        }
        else
        {
            output.WriteComment("Status is identical");
        }
    }

    private bool StatusDiffers(Study study, PipelineTrackerRow ingestedRow)
    {
        if (study.StudyStatusId != ingestedRow.StudyStatus)
        {
            return true;
        }

        var history = study.StudyStatusHistories.OrderByDescending(i => i.Id).FirstOrDefault();
        if (history != null)
        {
            if (study.StudyStatusId != history.StudyStatusId)
            {
                throw new Exception($"Inconsistent study status history for RMS study ID {study.Id}");
            }

            if (study.StudyStatusId == StudyStatusType.Withdrawn)
            {
                if (history.StudyStatusReasonHistories.Count != 1)
                {
                    return true;
                }

                return history.StudyStatusReasonHistories.First().WithdrawnReasonId !=
                       ingestedRow.RejectedWithdrawnReasonType.WithdrawnReason;
            } 
            
            if (study.StudyStatusId == StudyStatusType.Rejected)
            {
                if (history.StudyStatusReasonHistories.Count != 1)
                {
                    return true;
                }

                return history.StudyStatusReasonHistories.First().RejectedReasonId !=
                       ingestedRow.RejectedWithdrawnReasonType.RejectedReason;
            }
        }

        return false;
    }

    protected static void WriteInsertStatusHistory(ISqlWriter output, ISqlValue studyId,
        StudyStatusType ingestedRowStudyStatus,
        RejectedWithdrawnReasonType? ingestedRowRejectedWithdrawnReasonType, int userId)
    {
        output.Insert("StudyStatusHistory", new
        {
            StudyId = studyId,
            StudyStatusId = ingestedRowStudyStatus,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedById = userId,
            UpdatedById = userId,
        });

        if (ingestedRowStudyStatus is StudyStatusType.Rejected or StudyStatusType.Withdrawn)
        {
            var studyStatusHistoryId = new SqlVariable("@studyStatusHistoryId");
            output.Set(studyStatusHistoryId, MySqlFunctions.LastInsertId());
            
            if (ingestedRowStudyStatus == StudyStatusType.Rejected)
            {
                output.Insert("StudyStatusReasonHistory", new
                {
                    StudyStatusHistoryId = studyStatusHistoryId,
                    RejectedReasonId = ingestedRowRejectedWithdrawnReasonType?.RejectedReason ?? RejectedReasonType.Misc,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedById = userId,
                    UpdatedById = userId,
                });
            }
            else if (ingestedRowStudyStatus == StudyStatusType.Withdrawn)
            {
                output.Insert("StudyStatusReasonHistory", new
                {
                    StudyStatusHistoryId = studyStatusHistoryId,
                    WithdrawnReasonId = ingestedRowRejectedWithdrawnReasonType?.WithdrawnReason ?? WithdrawnReasonType.Other,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedById = userId,
                    UpdatedById = userId,
                });
            }
        }
    }

    private void CreateAddMissingStudiesScript(List<PipelineTrackerRow> ingestedRows, List<Study> existingStudies,
        ISqlWriter output, int userId)
    {
        int validMatchCount = 0;
        foreach (var row in ingestedRows)
        {
            var candidates = MatchStudies(existingStudies, row);
            if (candidates.Length == 0)
            {
                WriteInsertStudy(output, row, userId);
                validMatchCount++;
            }
            else if (candidates.Length > 1)
            {
                logger.LogWarning("{studyMatchCount} studies matched ingested row with VS ID {VsId}", candidates.Length, row.VsId);
                throw new Exception("Multiple studies matched to row");
            }
        }
        
        logger.LogInformation("{rowCount} inserts written out of a total of {ingestCount} rows ingested and {studyCount} RMS studies", validMatchCount, ingestedRows.Count, existingStudies.Count);

    }

    private void WriteInsertStudy(ISqlWriter output, PipelineTrackerRow ingestedRow, int userId)
    {
        output.WriteComment($"VS ID: {ingestedRow.VsId}");

        output.Insert("Studies", new
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
                StudyStatusId = ingestedRow.StudyStatus,
                RecruitmentStartDate = ingestedRow.RecruitmentStartDate,
                RecruitmentEndDate = ingestedRow.RecruitmentEndDate,
                RecruitmentTarget = ingestedRow.OverallRecruitmentTarget,
                NewApplicationAt = ingestedRow.AddedToRegistryPipelineAt,
                IsRecruitingIdentifiableParticipants = false,
                IsDeleted = false
            });

        var studyIdVar = new SqlVariable("@StudyId");
        output.Set(studyIdVar, MySqlFunctions.LastInsertId());

        WriteInsertStatusHistory(output, studyIdVar, ingestedRow.StudyStatus,
            ingestedRow.RejectedWithdrawnReasonType, userId);
    }
}