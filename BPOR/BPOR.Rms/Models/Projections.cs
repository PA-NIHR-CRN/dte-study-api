using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Models.Volunteer;
using EmailCampaign = BPOR.Rms.Models.Study.EmailCampaign;
using EmailCampaignParticipant = BPOR.Rms.Models.Study.EmailCampaignParticipant;

namespace BPOR.Rms.Models;

public static class Projections
{
    public static IQueryable<StudyModel> AsStudyListModel(this IQueryable<Domain.Entities.Study> source) =>
        source.Select(StudyAsStudyListModel());

    public static IQueryable<StudyDetailsViewModel> AsStudyDetailsViewModel(
        this IQueryable<Domain.Entities.Study> source) => source.Select(StudyAsStudyDetailsViewModel());

    public static IQueryable<EnrollmentDetails> AsEnrollmentDetails(this IQueryable<ManualEnrollment> source) =>
        source.Select(ManualEnrollmentToEnrollmentDetails());

    public static IQueryable<StudyFormViewModel> AsStudyFormViewModel(this IQueryable<Domain.Entities.Study> source,
        int step, bool isEditMode) => source.Select(StudyAsStudyFormViewModel(step, isEditMode));

    public static Expression<Func<Domain.Entities.Study, StudyFormViewModel>> StudyAsStudyFormViewModel(int step,
        bool isEditMode)
    {
        return s => new StudyFormViewModel
        {
            Id = s.Id,
            FullName = s.FullName,
            EmailAddress = s.EmailAddress,
            StudyName = s.StudyName,
            CpmsId = s.CpmsId,
            Step = step,
            IsEditMode = isEditMode
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
            },
            EnrollmentDetails = GetEnrollmentDetails(s.ManualEnrollments),

            EmailCampaigns = s.FilterCriterias
                .SelectMany(fc => fc.EmailCampaigns)
                .Select(ec => new EmailCampaign
                {
                    TargetGroupSize = (int)ec.TargetGroupSize,
                    CreatedAt = ec.CreatedAt,
                    Name = ec.Name,
                    EmailCampaignParticipants = ec.Participants
                        .Select(p => new EmailCampaignParticipant
                        {
                            ContactEmail = p.ContactEmail,
                            SentAt = p.SentAt,
                            RegisteredInterestAt = p.RegisteredInterestAt,
                            DeliveredAt = p.DeliveredAt
                        })
                        .ToList(),
                })
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
}
