using System.Linq.Expressions;
using BPOR.Rms.Models.Study;

namespace BPOR.Rms.Models;

public static class Projections
{
    public static IQueryable<StudyModel> AsStudyListModel(this IQueryable<Domain.Entities.Study> source) => source.Select(StudyAsStudyListModel());

    public static Expression<Func<Domain.Entities.Study, StudyModel>> StudyAsStudyListModel()
    {
        return s => new StudyModel
        {
            Id = s.Id,
            FullName = s.FullName,
            EmailAddress = s.EmailAddress,
            StudyName = s.StudyName,
            CpmsId = s.CpmsId,
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
        };
    }
}
