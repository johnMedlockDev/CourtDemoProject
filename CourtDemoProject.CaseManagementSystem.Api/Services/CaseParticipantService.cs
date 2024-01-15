using CourtDemoProject.CaseManagementSystem.Data;
using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Api.Services;

public class CaseParticipantService(CaseManagementSystemDbContext context)
{
    public async Task<IEnumerable<CaseParticipantDto>> GetCaseParticipantsAsync()
    {
        return await context.CaseParticipants.Select(caseParticipant => caseParticipant.ToDto())
                             .ToListAsync();
    }

    public async Task<CaseParticipantDto?> GetCaseParticipantEntityAsync(Guid id)
    {
        var caseParticipant = await context.CaseParticipants.FindAsync(id);
        return caseParticipant?.ToDto();
    }

    public async Task<CaseParticipantDto> AddCaseParticipantEntityAsync(CaseParticipantDto caseParticipantDto)
    {
        var entity = caseParticipantDto.ToEntity();
        context.CaseParticipants.Add(entity);
        await context.SaveChangesAsync();
        return caseParticipantDto;
    }

    public async Task<bool> UpdateCaseParticipantEntityAsync(CaseParticipantDto caseParticipantDto)
    {
        var entity = caseParticipantDto.ToEntity();
        context.Entry(entity).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CaseParticipantEntityExists(caseParticipantDto.CaseParticipantEntityId)) return false;
            else throw;
        }
    }

    public async Task<bool> DeleteCaseParticipantEntityAsync(Guid id)
    {
        var entity = await context.CaseParticipants.FindAsync(id);
        if (entity == null) return false;

        context.CaseParticipants.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    private bool CaseParticipantEntityExists(Guid id)
    {
        return context.CaseParticipants.Any(e => e.CaseParticipantEntityId == id);
    }
}
