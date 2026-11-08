namespace BPOR.Rms.Infrastructure.EntityFrameworkCore;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
