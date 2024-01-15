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

    public async Task<CaseParticipantDto?> GetCaseParticipantAsync(Guid id)
    {
        var caseParticipant = await context.CaseParticipants.FindAsync(id);
        return caseParticipant?.ToDto();
    }

    public async Task<CaseParticipantDto> AddCaseParticipantAsync(CaseParticipantDto caseParticipantDto)
    {
        var entity = caseParticipantDto.ToEntity();
        _ = context.CaseParticipants.Add(entity);
        _ = await context.SaveChangesAsync();
        return caseParticipantDto;
    }

    public async Task<bool> UpdateCaseParticipantAsync(CaseParticipantDto caseParticipantDto)
    {
        var entity = await context.CaseParticipants.FindAsync(caseParticipantDto.CaseParticipantEntityId);
        if (entity == null)
        {
            return false;
        }

        // Update properties
        entity.CaseParticipantType = caseParticipantDto.CaseParticipantType;
        entity.CaseParticipantFirstName = caseParticipantDto.CaseParticipantFirstName;
        entity.CaseParticipantMiddleName = caseParticipantDto.CaseParticipantMiddleName;
        entity.CaseParticipantLastName = caseParticipantDto.CaseParticipantLastName;

        try
        {
            _ = await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CaseParticipantExists(entity.CaseParticipantEntityId))
            {
                return false;
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<bool> DeleteCaseParticipantAsync(Guid id)
    {
        var entity = await context.CaseParticipants.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _ = context.CaseParticipants.Remove(entity);
        _ = await context.SaveChangesAsync();
        return true;
    }

    private bool CaseParticipantExists(Guid id)
    {
        return context.CaseParticipants.Any(e => e.CaseParticipantEntityId == id);
    }
}
