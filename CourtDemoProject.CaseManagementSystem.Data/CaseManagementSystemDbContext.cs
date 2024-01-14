
using CourtDemoProject.CaseManagementSystem.Data.Configurations;
using CourtDemoProject.CaseManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Data;

public class CaseManagementSystemDbContext(DbContextOptions<CaseManagementSystemDbContext> options) : DbContext(options)
{
    public DbSet<CaseEntity> Cases { get; set; }
    public DbSet<CaseDetailEntity> CaseDetails { get; set; }
    public DbSet<CaseParticipantEntity> CaseParticipants { get; set; }
    public DbSet<ChargeEntity> Charges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CaseConfiguration.Configure(modelBuilder);
        CaseDetailConfiguration.Configure(modelBuilder);
        CaseParticipantConfiguration.Configure(modelBuilder);
        ChargeConfiguration.Configure(modelBuilder);
    }
}
