using CourtDemoProject.CaseManagementSystem.Data.Entities;
using CourtDemoProject.CaseManagementSystem.Data.Enums;

namespace CourtDemoProject.CaseManagementSystem.Data.Dtos;
public record CaseParticipantDto(Guid CaseParticipantEntityId, CaseParticipantTypeEnum CaseParticipantType, string CaseParticipantFirstName, string CaseParticipantLastName, string CaseParticipantMiddleName)
{
    public CaseParticipantEntity ToEntity()
    {
        return new CaseParticipantEntity { CaseParticipantEntityId = CaseParticipantEntityId, CaseParticipantFirstName = CaseParticipantFirstName, CaseParticipantLastName = CaseParticipantLastName, CaseParticipantMiddleName = CaseParticipantMiddleName, CaseParticipantType = CaseParticipantType };
    }
}