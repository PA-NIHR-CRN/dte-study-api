using BPOR.Rms.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using MySqlConnector;

namespace BPOR.Rms.Utilities;

public class DbExceptionHelper : IDbExceptionHelper
{
    public bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        if (ex.InnerException is DbException dbEx)
        {
            if (dbEx is MySqlException mysqlEx && mysqlEx.Number == 1062)
                return true;
        }

        return false;
    }
}
