using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.Utilities.Interfaces;

public interface IDbExceptionHelper
{
    bool IsUniqueConstraintViolation(DbUpdateException ex);
}
