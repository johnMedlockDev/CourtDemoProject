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

    public async Task<ChargeDto?> GetChargeAsync(Guid id)
    {
        var charge = await context.Charges.FindAsync(id);

        return charge?.ToDto();
    }

    public async Task<ChargeDto> AddChargeAsync(ChargeDto chargeDto)
    {
        var entity = chargeDto.ToEntity();

        _ = context.Charges.Add(entity);
        _ = await context.SaveChangesAsync();

        return entity.ToDto();
    }

    public async Task<bool> UpdateChargeAsync(ChargeDto chargeDto)
    {
        var entity = await context.Charges.FindAsync(chargeDto.ChargeId);
        if (entity == null)
        {
            return false;
        }

        // Update properties
        entity.ChargeName = chargeDto.ChargeName;
        entity.ChargeCode = chargeDto.ChargeCode;
        entity.ChargeType = chargeDto.ChargeType;
        entity.SentenceLengthIndays = chargeDto.SentenceLengthIndays;

        try
        {
            _ = await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ChargeExists(entity.ChargeId))
            {
                return false;
            }

            throw;
        }
    }

    public async Task<bool> DeleteChargeAsync(Guid id)
    {
        var entity = await context.Charges.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        _ = context.Charges.Remove(entity);
        _ = await context.SaveChangesAsync();

        return true;
    }

    private bool ChargeExists(Guid id)
    {
        return context.Charges.Any(e => e.ChargeId == id);
    }
}
