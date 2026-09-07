namespace NIHR.Infrastructure.EntityFrameworkCore;

public abstract class ReferenceData<TId> : IReferenceData<TId>, ISoftDelete
    where TId : struct, Enum
{
    public TId Id { get; set; }
    int IReferenceData.Id => GetIdAsInt();
    public required string Code { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    protected abstract int GetIdAsInt();
}