using CourtDemoProject.CaseManagementSystem.Data.Entities;
using CourtDemoProject.CaseManagementSystem.Data.Enums;

namespace CourtDemoProject.CaseManagementSystem.Data.Dtos;
public record CaseDto(string CaseId, string CourtName, CaseTypeEnum CaseType, ICollection<CaseParticipantEntity> CaseParticipants, ICollection<ChargeEntity> Charges, DateOnly DateOfOffense, ICollection<CaseDetailEntity> CaseDetails, VerdictEnum Verdict, PleadEnum Plead, ICollection<DateTime> CourtDates, CaseStatusEnum CaseStatus);