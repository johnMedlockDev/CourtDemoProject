using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using CourtDemoProject.CaseManagementSystem.Data.Enums;

namespace CourtDemoProject.CaseManagementSystem.Data.Entities;
public class CaseEntity
{
    public string CaseId { get; set; } = null!;

    public string CourtName { get; set; } = null!;

    public CaseTypeEnum CaseType { get; set; }

    public ICollection<CaseParticipantEntity> CaseParticipants { get; set; } = new List<CaseParticipantEntity>();

    public ICollection<ChargeEntity> Charges { get; set; } = new List<ChargeEntity>();

    public DateOnly DateOfOffense { get; set; }

    public ICollection<CaseDetailEntity> CaseDetails { get; set; } = new List<CaseDetailEntity>();

    public VerdictEnum Verdict { get; set; }
    public PleadEnum Plead { get; set; }

    public ICollection<DateTime> CourtDates { get; set; } = new List<DateTime>();

    public CaseStatusEnum CaseStatus { get; set; }

    public CaseDto ToDto()
    {
        return new CaseDto(CaseId, CourtName, CaseType, ConvertCaseParticipantEntitiesToDto, ConvertChargeEntitiesToDto, DateOfOffense, ConvertCaseDetailEntitiesToDto, Verdict, Plead, CourtDates, CaseStatus);
    }

    private ICollection<CaseParticipantDto> ConvertCaseParticipantEntitiesToDto => CaseParticipants.Select(x => x.ToDto()).ToList();
    private ICollection<ChargeDto> ConvertChargeEntitiesToDto => Charges.Select(x => x.ToDto()).ToList();
    private ICollection<CaseDetailDto> ConvertCaseDetailEntitiesToDto => CaseDetails.Select(x => x.ToDto()).ToList();
}
