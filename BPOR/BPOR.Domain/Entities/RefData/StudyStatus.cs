using BPOR.Domain.Enums;
using BPOR.Rms.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities.RefData;

public class StudyStatus : ReferenceData<StudyStatusType>
{
    protected override int GetIdAsInt() => (int)Id;
}