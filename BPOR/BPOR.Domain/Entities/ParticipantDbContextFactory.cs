using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NIHR.Infrastructure.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace BPOR.Domain.Entities;

public class ParticipantDbContextFactory() : IDesignTimeDbContextFactory<ParticipantDbContext>
{
    public ParticipantDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ParticipantDbContext>()
            .UseMySql(ServerVersion.Create(new Version(8, 0, 40), ServerType.MySql), x =>
            {
                x.UseNetTopologySuite();
                x.CommandTimeout(300);
            })
            .Options;


        return new ParticipantDbContext(options);
    }
}
