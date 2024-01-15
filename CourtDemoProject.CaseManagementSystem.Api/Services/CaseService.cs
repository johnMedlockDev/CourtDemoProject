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

    public async Task<CaseDto?> GetCaseAsync(string id)
    {
        var caseEntity = await context.Cases.FindAsync(id);
        return caseEntity?.ToDto();
    }

    public async Task<CaseDto> AddCaseAsync(CaseDto caseDto)
    {
        var entity = caseDto.ToEntity();

        _ = context.Cases.Add(entity);
        _ = await context.SaveChangesAsync();

        return caseDto;
    }

    public async Task<bool> UpdateCaseAsync(CaseDto caseDto)
    {
        var entity = await context.Cases.FindAsync(caseDto.CaseId);
        if (entity == null)
        {
            return false;
        }

        // Update properties
        entity.CourtName = caseDto.CourtName;
        entity.CaseType = caseDto.CaseType;

        entity.CaseParticipants = caseDto.CaseParticipants.Select(e => e.ToEntity()).ToList();
        entity.Charges = caseDto.Charges.Select(e => e.ToEntity()).ToList();
        entity.DateOfOffense = caseDto.DateOfOffense;
        entity.CaseDetails = caseDto.CaseDetails.Select(e => e.ToEntity()).ToList();
        entity.Verdict = caseDto.Verdict;
        entity.Plead = caseDto.Plead;
        entity.CourtDates = caseDto.CourtDates;
        entity.CaseStatus = caseDto.CaseStatus;

        try
        {
            _ = await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CaseExists(entity.CaseId))
            {
                return false;
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<bool> DeleteCaseAsync(string id)
    {
        var caseEntity = await context.Cases.FindAsync(id);
        if (caseEntity == null)
        {
            return false;
        }

        _ = context.Cases.Remove(caseEntity);
        _ = await context.SaveChangesAsync();
        return true;
    }

    private bool CaseExists(string id)
    {
        return context.Cases.Any(e => e.CaseId == id);
    }
}