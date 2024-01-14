namespace CourtDemoProject.CaseManagementSystem.Data.Entities;

public class CaseDetailEntity
{
    public Guid CaseDetailId { get; set; }
    public DateTime CaseDetailEntryDateTime { get; set; }
    public string Description { get; set; } = "None";
    public string DocketDetail { get; set; } = "None";
    public Uri? DocumentUri { get; set; }
}