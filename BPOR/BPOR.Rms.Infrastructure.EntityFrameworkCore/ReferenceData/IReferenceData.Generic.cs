namespace BPOR.Rms.Infrastructure.EntityFrameworkCore;

public interface IReferenceData<TId> : IReferenceData
    where TId : struct, Enum
{
    new TId Id { get; set; }
}
