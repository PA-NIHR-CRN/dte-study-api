using System.Linq.Expressions;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Models.Volunteer;

namespace BPOR.Rms.Models;

public static class Projections
{
    private static IEnumerable<EnrollmentDetails> GetEnrollmentDetails(
        IEnumerable<Domain.Entities.ManualEnrollment> enrollments)
    {
        return enrollments
            .Where(e => !e.IsDeleted)
            .OrderByDescending(e => e.CreatedAt) // This orders the entries from oldest to newest based on the CreatedAt date
            .Select(e => new EnrollmentDetails
            {
                RecruitmentTotal = e.TotalEnrollments,
                CreatedAt = e.CreatedAt,
            });
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
                .Where(e => !e.IsDeleted)
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
            EnrollmentDetails = GetEnrollmentDetails(s.ManualEnrollments)
        };
    }
}
