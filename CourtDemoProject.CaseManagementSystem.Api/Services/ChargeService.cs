using CourtDemoProject.CaseManagementSystem.Data;
using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CourtDemoProject.CaseManagementSystem.Api.Services;

public class ChargeService(CaseManagementSystemDbContext context)
{
    public async Task<IEnumerable<ChargeDto>> GetChargesAsync()
    {
        return await context.Charges.Select(e => e.ToDto()).ToListAsync();
    }

    public async Task<ChargeDto?> GetChargeEntityAsync(Guid id)
    {
        var charge = await context.Charges.FindAsync(id);

        return charge?.ToDto();
    }

    public async Task<ChargeDto> AddChargeEntityAsync(ChargeDto chargeDto)
    {
        var entity = chargeDto.ToEntity();

        context.Charges.Add(entity);
        await context.SaveChangesAsync();

        return entity.ToDto();
    }

    public async Task<bool> UpdateChargeEntityAsync(ChargeDto chargeDto)
    {
        var entity = chargeDto.ToEntity();
        context.Entry(entity).State = EntityState.Modified;
        try
        {
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ChargeEntityExists(entity.ChargeId))
            {
                return false;
            }
            throw;
        }
    }

    public async Task<bool> DeleteChargeEntityAsync(Guid id)
    {
        var entity = await context.Charges.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        context.Charges.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    private bool ChargeEntityExists(Guid id)
    {
        return context.Charges.Any(e => e.ChargeId == id);
    }
}
