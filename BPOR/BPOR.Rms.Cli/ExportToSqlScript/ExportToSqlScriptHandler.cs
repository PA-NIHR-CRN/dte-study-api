using System.CommandLine;
using Albatross.CommandLine;
using Bogus;
using BPOR.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NIHR.SqlWriter;

namespace BPOR.Rms.Cli.ExportToSqlScript;

public class ExportToSqlScriptHandler(
    ParseResult parseResult,
    ExportToSqlScriptParams parameters,
    ParticipantDbContext dbContext) : BaseHandler<ExportToSqlScriptParams>(parseResult, parameters)
{
    public sealed override async Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        var dbConnection = dbContext.Database.GetDbConnection();
        var studies = await dbConnection.QueryAsync<Study>("select * from Studies where IsDeleted = 0");
        using var output = File.CreateText(Path.Join(parameters.Destination ?? ".",
            $"{DateTime.UtcNow:yyyy-MM-ddTHH_mm_ss}-insert-studies.sql"));
        var writer = new MySqlSqlWriter(output);
        var faker = new Faker();

        writer.InsertBatch(
        "Studies",
        studies.Select(study => new
        {
            study.FullName,
            EmailAddress = faker.Internet.Email(provider: "test.nihr.ac.uk"),
            study.StudyName,
            study.CpmsId,
            study.IsRecruitingIdentifiableParticipants,
            IsDeleted = 0,
            study.CreatedAt,
            study.UpdatedAt,
            CreatedById = parameters.UserId,
            UpdatedById = parameters.UserId,
            study.AlreadyOpenToRecruitment,
            study.ChiefInvestigator,
            study.FundingCode,
            study.HasNihrFunding,
            study.ParticipantsRecruited,
            study.RecruitmentEndDate,
            study.RecruitmentStartDate,
            study.RecruitmentTarget,
            study.Sponsors,
            study.SubmissionOutcomeId,
            study.SubmittedId,
            study.TargetPopulation,
            study.InformationUrl,
            study.HasMultipleResearchLocations,
            study.PreScreenerUrl,
            study.SinglePersonResponsibleForRecruiting,
            ChiefInvestigatorEmail = faker.Internet.Email(provider: "test.nihr.ac.uk"),
            study.StudyStatusId
            }));
        

        return 1;
    }
}