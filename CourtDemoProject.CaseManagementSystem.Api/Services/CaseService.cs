using CourtDemoProject.CaseManagementSystem.Data;
using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Api.Services;

public class CaseService(CaseManagementSystemDbContext context)
{
    public async Task<IEnumerable<CaseDto>> GetCasesAsync()
    {
        return await context.Cases.Select(e => e.ToDto()).ToListAsync();
    }

    public async Task<CaseDto?> GetCaseEntityAsync(string id)
    {
        var caseEntity = await context.Cases.FindAsync(id);
        return caseEntity?.ToDto();
    }

    public async Task<CaseDto> AddCaseEntityAsync(CaseDto caseDto)
    {
        var entity = caseDto.ToEntity();

        context.Cases.Add(entity);
        await context.SaveChangesAsync();

        return caseDto;
    }

    public async Task<bool> UpdateCaseEntityAsync(CaseDto caseDto)
    {
        var entity = caseDto.ToEntity();
        context.Entry(entity).State = EntityState.Modified;
        try
        {
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CaseEntityExists(entity.CaseId)) return false;
            else throw;
        }
    }

    public async Task<bool> DeleteCaseEntityAsync(string id)
    {
        var caseEntity = await context.Cases.FindAsync(id);
        if (caseEntity == null) return false;

        context.Cases.Remove(caseEntity);
        await context.SaveChangesAsync();
        return true;
    }

    private bool CaseEntityExists(string id)
    {
        return context.Cases.Any(e => e.CaseId == id);
    }
}