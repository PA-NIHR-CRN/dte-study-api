using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Models.Volunteer;

namespace BPOR.Rms.Models;

public static class Projections
{
    public static IQueryable<StudyModel> AsStudyListModel(this IQueryable<Domain.Entities.Study> source) => source.Select(StudyAsStudyListModel());
    public static IQueryable<StudyDetailsViewModel> AsStudyDetailsViewModel(this IQueryable<Domain.Entities.Study> source) => source.Select(StudyAsStudyDetailsViewModel());

    public static IQueryable<EnrollmentDetails> AsEnrollmentDetails(this IQueryable<ManualEnrollment> source) => source.Select(ManualEnrollmentToEnrollmentDetails());

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
                .FirstOrDefault()
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
        };
    }

    public static Expression<Func<Domain.Entities.Study, UpdateAnonymousRecruitedViewModel>>
        StudyAsUpdateAnonymousRecruitedViewModel()
    {
        return s => new UpdateAnonymousRecruitedViewModel
        {
            StudyId = s.Id,
            StudyName = s.StudyName,
            EnrollmentDetails = GetEnrollmentDetails(s.ManualEnrollments).OrderByDescending(e => e.CreatedAt)
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
                CreatedAt = e.CreatedAt,
            });
    }
}
