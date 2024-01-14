using CourtDemoProject.CaseManagementSystem.Data.Enums;

namespace CourtDemoProject.CaseManagementSystem.Data.Entities;

public class CaseParticipantEntity
{
    public Guid CaseParticipantEntityId { get; set; }
    public CaseParticipantTypeEnum CaseParticipantType { get; set; }
    public string CaseParticipantFirstName { get; set; } = null!;
    public string CaseParticipantLastName { get; set; } = null!;
    public string CaseParticipantMiddleName { get; set; } = String.Empty;
}