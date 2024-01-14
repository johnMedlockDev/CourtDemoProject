namespace CourtDemoProject.CaseManagementSystem.Data.Dtos;
public record CaseDetailDto(Guid CaseDetailId, DateTime CaseDetailEntryDateTime, string Description, string DocketDetail, Uri? DocumentUri);