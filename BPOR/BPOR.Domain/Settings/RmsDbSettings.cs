using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Settings;

public class RmsDbSettings : DbSettings
{
    public static string SectionName => "DbSettings";
}