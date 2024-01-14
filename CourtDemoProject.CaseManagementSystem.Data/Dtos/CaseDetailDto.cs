using CourtDemoProject.CaseManagementSystem.Data.Entities;

namespace CourtDemoProject.CaseManagementSystem.Data.Dtos;
public record CaseDetailDto(Guid CaseDetailId, DateTime CaseDetailEntryDateTime, string Description, string DocketDetail, Uri? DocumentUri)
{
    public CaseDetailEntity ToEntity()
    {
        return new CaseDetailEntity() { CaseDetailId = CaseDetailId, CaseDetailEntryDateTime = CaseDetailEntryDateTime, Description = Description, DocketDetail = DocketDetail, DocumentUri = DocumentUri };
    }
}