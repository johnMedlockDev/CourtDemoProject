using CourtDemoProject.CaseManagementSystem.Data.Entities;
using CourtDemoProject.CaseManagementSystem.Data.Enums;

namespace CourtDemoProject.CaseManagementSystem.Data.Dtos;
public record ChargeDto(Guid ChargeId, string ChargeName, string ChargeCode, ChargeTypeEnum ChargeType, JudgementTypeEnum JudgementType, double FineAmount, int SentenceLengthIndays)
{
    public ChargeEntity ToEntity()
    {
        return new ChargeEntity { ChargeId = ChargeId, ChargeName = ChargeName, ChargeType = ChargeType, FineAmount = FineAmount, JudgementType = JudgementType, SentenceLengthIndays = SentenceLengthIndays, ChargeCode = ChargeCode };
    }
}