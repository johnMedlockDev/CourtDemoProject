using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CourtDemoProject.CaseManagementSystem.Data;

public class CaseManagementSystemDbContextFactory : IDesignTimeDbContextFactory<CaseManagementSystemDbContext>
{
    public CaseManagementSystemDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CaseManagementSystemDbContext>();
        var connectionString = args.Length > 0 ? args[1] : "";

        _ = optionsBuilder.UseSqlServer(connectionString);

        return new CaseManagementSystemDbContext(optionsBuilder.Options);
    }
}