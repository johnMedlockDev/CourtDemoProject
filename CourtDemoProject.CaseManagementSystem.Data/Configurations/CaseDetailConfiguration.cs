using CourtDemoProject.CaseManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Data.Configurations;
public static class CaseDetailConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<CaseDetailEntity>(entity =>
        {
            _ = entity.HasKey(e => e.CaseDetailId);
            _ = entity.Property(e => e.Description).HasMaxLength(200);
            _ = entity.Property(e => e.DocketDetail).HasMaxLength(200);
        });
    }
}
