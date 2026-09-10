using BPOR.Domain.Enums;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities.RefData;

public class NihrFundingStatus : ReferenceData<NihrFundingStatusType>
{
    protected override int GetIdAsInt() => (int)Id;
}