using CourtDemoProject.CaseManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Data.Configurations;
public static class CaseConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<CaseEntity>(entity =>
        {
            _ = entity.HasKey(e => e.CaseId);
            _ = entity.Property(e => e.CourtName).HasMaxLength(50);
        });
    }
}
