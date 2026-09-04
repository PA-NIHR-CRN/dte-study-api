using NIHR.Infrastructure.EntityFrameworkCore;
using BPOR.Domain.Enums;

namespace BPOR.Domain.Entities.RefData;

public class Submitted : ReferenceData<SubmittedType>
{
    protected override int GetIdAsInt() => (int)Id;
}