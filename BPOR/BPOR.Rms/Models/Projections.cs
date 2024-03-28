using System.Linq.Expressions;
using BPOR.Rms.Models.Study;

namespace BPOR.Rms.Models;

public static class Projections
{
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
}
