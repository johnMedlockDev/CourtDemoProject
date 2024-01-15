using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using CourtDemoProject.CaseManagementSystem.Data.Enums;

namespace CourtDemoProject.CaseManagementSystem.Data.Entities;
public class CaseEntity
{
    public string CaseId { get; set; } = null!;

    public string CourtName { get; set; } = null!;

    public CaseTypeEnum CaseType { get; set; }

    public List<CaseParticipantEntity> CaseParticipants { get; set; } = [];

    public List<ChargeEntity> Charges { get; set; } = [];

    public DateOnly DateOfOffense { get; set; }

    public List<CaseDetailEntity> CaseDetails { get; set; } = [];

    public VerdictEnum Verdict { get; set; }
    public PleadEnum Plead { get; set; }

    public List<DateTime> CourtDates { get; set; } = [];

    public CaseStatusEnum CaseStatus { get; set; }

    public CaseDto ToDto()
    {
        return new CaseDto(CaseId, CourtName, CaseType, ConvertCaseParticipantEntitiesToDto, ConvertChargeEntitiesToDto, DateOfOffense, ConvertCaseDetailEntitiesToDto, Verdict, Plead, CourtDates, CaseStatus);
    }

    private List<CaseParticipantDto> ConvertCaseParticipantEntitiesToDto => CaseParticipants.Select(x => x.ToDto()).ToList();
    private List<ChargeDto> ConvertChargeEntitiesToDto => Charges.Select(x => x.ToDto()).ToList();
    private List<CaseDetailDto> ConvertCaseDetailEntitiesToDto => CaseDetails.Select(x => x.ToDto()).ToList();
}
