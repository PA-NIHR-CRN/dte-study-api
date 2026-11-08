using BPOR.Domain.Enums;
using BPOR.Rms.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities.RefData;

public class WithdrawnReason : ReferenceData<WithdrawnReasonType>
{
    protected override int GetIdAsInt() => (int)Id;
}
