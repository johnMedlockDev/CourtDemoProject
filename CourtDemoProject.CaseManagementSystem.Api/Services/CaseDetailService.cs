using CourtDemoProject.CaseManagementSystem.Data;
using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Api.Services;

public class CaseDetailService(CaseManagementSystemDbContext context)
{
    public async Task<IEnumerable<CaseDetailDto>> GetCaseDetailsAsync()
    {
        return await context.CaseDetails
                             .Select(caseDetail => caseDetail.ToDto())
                             .ToListAsync();
    }

    public async Task<CaseDetailDto?> GetCaseDetailAsync(Guid id)
    {
        var caseDetail = await context.CaseDetails.FindAsync(id);
        return caseDetail?.ToDto();
    }

    public async Task<CaseDetailDto> AddCaseDetailAsync(CaseDetailDto caseDetailDto)
    {
        var caseDetail = caseDetailDto.ToEntity();
        _ = context.CaseDetails.Add(caseDetail);
        _ = await context.SaveChangesAsync();
        return caseDetail.ToDto();
    }

    public async Task<bool> UpdateCaseDetailAsync(CaseDetailDto caseDetailDto)
    {
        var entity = await context.CaseDetails.FindAsync(caseDetailDto.CaseDetailId);
        if (entity == null)
        {
            return false;
        }

        // Update properties
        entity.CaseDetailEntryDateTime = caseDetailDto.CaseDetailEntryDateTime;
        entity.Description = caseDetailDto.Description;
        entity.DocketDetail = caseDetailDto.DocketDetail;
        entity.DocumentUri = caseDetailDto.DocumentUri;

        try
        {
            _ = await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CaseDetailExists(entity.CaseDetailId))
            {
                return false;
            }
            else
            {
                throw;
            }
        }
    }

    public async Task DeleteCaseDetailAsync(Guid id)
    {
        var caseDetail = await context.CaseDetails.FindAsync(id) ?? throw new InvalidOperationException("Case detail not found");
        _ = context.CaseDetails.Remove(caseDetail);
        _ = await context.SaveChangesAsync();
    }

    public bool CaseDetailExists(Guid id)
    {
        return context.CaseDetails.Any(e => e.CaseDetailId == id);
    }
}