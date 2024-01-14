using CourtDemoProject.CaseManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Data.Configurations;
public static class CaseParticipantConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<CaseParticipantEntity>(entity =>
        {
            _ = entity.HasKey(e => e.CaseParticipantEntityId);
            _ = entity.Property(e => e.CaseParticipantFirstName).HasMaxLength(25);
            _ = entity.Property(e => e.CaseParticipantMiddleName).HasMaxLength(25);
            _ = entity.Property(e => e.CaseParticipantLastName).HasMaxLength(25);
        });
    }
}
