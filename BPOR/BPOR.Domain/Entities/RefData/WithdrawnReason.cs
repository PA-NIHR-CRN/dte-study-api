using BPOR.Domain.Enums;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities.RefData;

public class WithdrawnReason : ReferenceData<WithdrawnReasonType>
{
    protected override int GetIdAsInt() => (int)Id;
}
