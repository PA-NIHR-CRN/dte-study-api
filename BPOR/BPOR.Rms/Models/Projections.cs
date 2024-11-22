using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Researcher;
using NIHR.GovUk.AspNetCore.Mvc;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Models.Volunteer;
using EmailCampaign = BPOR.Rms.Models.Study.Campaign;
using EmailCampaignParticipant = BPOR.Rms.Models.Study.CampaignParticipant;

namespace BPOR.Rms.Models;

public static class Projections
{
    public static IQueryable<StudyModel> AsStudyListModel(this IQueryable<Domain.Entities.Study> source) =>
        source.Select(StudyAsStudyListModel());

    public static IQueryable<StudyDetailsViewModel> AsStudyDetailsViewModel(
        this IQueryable<Domain.Entities.Study> source) => source.Select(StudyAsStudyDetailsViewModel());

    public static IQueryable<EnrollmentDetails> AsEnrollmentDetails(this IQueryable<ManualEnrollment> source) =>

        source.Select(ManualEnrollmentToEnrollmentDetails());

    public static IQueryable<EmailParticipantDetails> AsEmailCampaignParticipant(this IQueryable<Participant> source) =>

        source.Select(VolunteerToEmailParticipantDetails());

    public static IQueryable<StudyFormViewModel> AsStudyFormViewModel(this IQueryable<Domain.Entities.Study> source) => source.Select(StudyAsStudyFormViewModel());

    public static IQueryable<ResearcherStudyFormViewModel> AsResearcherFormViewModel(
        this IQueryable<Domain.Entities.Study> source) =>
        source.Select(StudyAsResearcherFormViewModel());

    public static Expression<Func<Domain.Entities.Study, ResearcherStudyFormViewModel>> StudyAsResearcherFormViewModel()
    {
        return r => new ResearcherStudyFormViewModel
        {
            Id = r.Id,
            ChiefInvestigator = r.ChiefInvestigator,
            StudySponsors = r.Sponsors,
            HasFunding = r.HasNihrFunding.Value,
            FundingCode = r.FundingCode,
            UKRecruitmentTarget = r.RecruitmentTarget,
            TargetPopulation = r.TargetPopulation,
            RecruitmentStartDate = GovUkDate.FromDateTime(r.RecruitmentStartDate),
            RecruitmentEndDate = GovUkDate.FromDateTime(r.RecruitmentEndDate),
            ShortName = r.StudyName,
            CPMSId = r.CpmsId,
            RecruitingIdentifiableVolunteers = r.IsRecruitingIdentifiableParticipants,
            OutcomeOfSubmission = r.SubmissionOutcomeId,
            PortfolioSubmissionStatus = r.SubmittedId,
        };
    }

    public static Expression<Func<Domain.Entities.Study, StudyFormViewModel>> StudyAsStudyFormViewModel()
    {
        return s => new StudyFormViewModel
        {
            Id = s.Id,
            FullName = s.FullName,
            EmailAddress = s.EmailAddress,
            StudyName = s.StudyName,
            CpmsId = s.CpmsId
        };
    }

    public static Expression<Func<Domain.Entities.Study, StudyModel>> StudyAsStudyListModel()
    {
        return s => new StudyModel
        {
            Id = s.Id,
            FullName = s.FullName,
            EmailAddress = s.EmailAddress,
            StudyName = s.StudyName,
            CpmsId = s.CpmsId,
            IsRecruitingIdentifiableParticipants = s.IsRecruitingIdentifiableParticipants,
            LatestRecruitmentTotal = s.ManualEnrollments
                .OrderByDescending(e => e.CreatedAt)
                .Select(e => e.TotalEnrollments)
                .FirstOrDefault(),
            TotalRecruited = s.ManualEnrollments
                .Where(m => m.StudyId == s.Id)
                .Sum(e => e.TotalEnrollments)
        };
    }

    public static Expression<Func<Domain.Entities.Study, StudyDetailsViewModel>> StudyAsStudyDetailsViewModel()
    {
        return s => new StudyDetailsViewModel
        {
            Study = new StudyModel
            {
                Id = s.Id,
                FullName = s.FullName,
                EmailAddress = s.EmailAddress,
                StudyName = s.StudyName,
                CpmsId = s.CpmsId,
                IsRecruitingIdentifiableParticipants = s.IsRecruitingIdentifiableParticipants,
                ChiefInvestigator = s.ChiefInvestigator,
                StudySponsors = s.Sponsors,
                HasFunding = s.HasNihrFunding.Value,
                OutcomeOfSubmission = s.SubmissionOutcome != null ? s.SubmissionOutcome.Code : null,
                PortfolioSubmissionStatus = s.Submitted != null ? s.Submitted.Code : null,
                FundingCode = s.FundingCode,
                UKRecruitmentTarget = s.RecruitmentTarget,
                TargetPopulation = s.TargetPopulation,
                RecruitmentStartDate = GovUkDate.FromDateTime(s.RecruitmentStartDate).UKDisplayDate(),
                RecruitmentEndDate = GovUkDate.FromDateTime(s.RecruitmentEndDate).UKDisplayDate(),
                InformationUrl = s.InformationUrl,
            },
            EnrollmentDetails = GetEnrollmentDetails(s.ManualEnrollments),

            Campaigns = s.FilterCriterias
                .SelectMany(fc => fc.Campaigns)
                .Select(ec => new EmailCampaign
                {
                    TargetGroupSize = (int)ec.TargetGroupSize,
                    CreatedAt = ec.CreatedAt,
                    Name = ec.Name,
                    CampaignParticipants = ec.Participants
                        .Select(p => new EmailCampaignParticipant
                        {
                            SentAt = p.SentAt,
                            RegisteredInterestAt = p.RegisteredInterestAt,
                            DeliveredAt = p.DeliveredAt,
                            DeliveryStatusId = p.DeliveryStatusId
                        })
                        .ToList(),
                }),
            HasCampaigns = s.FilterCriterias.Any(fc => fc.Campaigns.Any())
        };
    }


    public static Expression<Func<Domain.Entities.Study, UpdateAnonymousRecruitedViewModel>>
        StudyAsUpdateAnonymousRecruitedViewModel()
    {
        return s => new UpdateAnonymousRecruitedViewModel
        {
            StudyId = s.Id,
            StudyName = s.StudyName,
            EnrollmentDetails = GetEnrollmentDetails(s.ManualEnrollments).AsEnumerable()
        };
    }

    private static Expression<Func<ManualEnrollment, EnrollmentDetails>> ManualEnrollmentToEnrollmentDetails()
    {
        return e => new EnrollmentDetails
        {
            RecruitmentTotal = e.TotalEnrollments,
            CreatedAt = e.CreatedAt,
        };
    }

    private static IEnumerable<EnrollmentDetails> GetEnrollmentDetails(
        IEnumerable<Domain.Entities.ManualEnrollment> enrollments)
    {
        return enrollments
            .OrderByDescending(e => e.CreatedAt)
            .Select(e => new EnrollmentDetails
            {
                RecruitmentTotal = e.TotalEnrollments,
                CreatedAt = e.CreatedAt
            }).AsEnumerable();
    } 
    private static Expression<Func<Participant, EmailParticipantDetails >> VolunteerToEmailParticipantDetails()

    {
        return v => new EmailParticipantDetails
        {
            Id = v.Id,
            Email = v.Email,
            FirstName = v.FirstName,
            LastName = v.LastName
        };
    }
}