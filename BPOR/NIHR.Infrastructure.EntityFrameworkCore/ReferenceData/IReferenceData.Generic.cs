namespace NIHR.Infrastructure.EntityFrameworkCore;

public interface IReferenceData<TId> : IReferenceData
    where TId : struct, Enum
{
    new TId Id { get; set; }
}
