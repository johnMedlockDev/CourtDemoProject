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
        context.CaseDetails.Add(caseDetail);
        await context.SaveChangesAsync();
        return caseDetail.ToDto();
    }

    public async Task<bool> UpdateCaseDetailAsync(CaseDetailDto caseDetailDto)
    {
        var entity = caseDetailDto.ToEntity();
        context.Entry(entity).State = EntityState.Modified;
        try
        {
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CaseDetailExists(entity.CaseDetailId)) return false;
            else throw;
        }
    }

    public async Task DeleteCaseDetailAsync(Guid id)
    {
        var caseDetail = await context.CaseDetails.FindAsync(id);
        if (caseDetail == null)
            throw new InvalidOperationException("Case detail not found");

        context.CaseDetails.Remove(caseDetail);
        await context.SaveChangesAsync();
    }

    public bool CaseDetailExists(Guid id)
    {
        return context.CaseDetails.Any(e => e.CaseDetailId == id);
    }
}