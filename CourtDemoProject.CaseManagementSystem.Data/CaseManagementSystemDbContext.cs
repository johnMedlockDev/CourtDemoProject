
using CourtDemoProject.CaseManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Data;

public class CaseManagementSystemDbContext(DbContextOptions<CaseManagementSystemDbContext> options) : DbContext(options)
{
    public DbSet<CaseEntity> Cases { get; set; }
    public DbSet<CaseDetailEntity> CaseDetails { get; set; }
    public DbSet<CaseParticipantEntity> CaseParticipants { get; set; }
    public DbSet<ChargeEntity> Charges { get; set; }
}
