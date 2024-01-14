using CourtDemoProject.CaseManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Data.Configurations;
public static class ChargeConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<ChargeEntity>(entity =>
        {
            _ = entity.HasKey(e => e.ChargeId);
            _ = entity.Property(e => e.ChargeName).HasMaxLength(25);
            _ = entity.Property(e => e.ChargeCode).HasMaxLength(25);
        });
    }
}
