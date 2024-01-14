using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CourtDemoProject.CaseManagementSystem.Data;

public class CaseManagementSystemDbContextFactory : IDesignTimeDbContextFactory<CaseManagementSystemDbContext>
{
    public CaseManagementSystemDbContext CreateDbContext(string[] args)
    {
        var connectionString = args.Length > 1 && args[0] == "--connection-string" ? args[1] : "";

        var optionsBuilder = new DbContextOptionsBuilder<CaseManagementSystemDbContext>();
        _ = optionsBuilder.UseSqlServer(connectionString);

        return new CaseManagementSystemDbContext(optionsBuilder.Options);
    }
}