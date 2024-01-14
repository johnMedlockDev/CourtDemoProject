using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using CourtDemoProject.CaseManagementSystem.Data.Enums;

namespace CourtDemoProject.CaseManagementSystem.Data.Entities;

public class ChargeEntity
{
    public Guid ChargeId { get; set; }
    public string ChargeName { get; set; } = null!;
    public string ChargeCode { get; set; } = null!;
    public ChargeTypeEnum ChargeType { get; set; }
    public JudgementTypeEnum JudgementType { get; set; }
    public double FineAmount { get; set; }
    public int SentenceLengthIndays { get; set; }
    public ChargeDto ToDto()
    {
        return new ChargeDto(ChargeId, ChargeName, ChargeCode, ChargeType, JudgementType, FineAmount, SentenceLengthIndays);
    }
}