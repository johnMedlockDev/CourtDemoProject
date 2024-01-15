using CourtDemoProject.CaseManagementSystem.Api.Services;
using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CourtDemoProject.CaseManagementSystem.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ChargesController(ChargeService chargeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChargeDto>>> GetCharges()
    {
        var charges = await chargeService.GetChargesAsync();
        return Ok(charges);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChargeDto>> GetChargeEntity(Guid id)
    {
        var chargeDto = await chargeService.GetChargeEntityAsync(id);
        if (chargeDto == null)
        {
            return NotFound();
        }
        return Ok(chargeDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutChargeEntity(Guid id, ChargeDto chargeDto)
    {
        if (id != chargeDto.ChargeId)
        {
            return BadRequest();
        }

        var result = await chargeService.UpdateChargeEntityAsync(chargeDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<ChargeDto>> PostChargeEntity(ChargeDto chargeDto)
    {
        var createdDto = await chargeService.AddChargeEntityAsync(chargeDto);
        return CreatedAtAction(nameof(GetChargeEntity), new { id = createdDto.ChargeId }, createdDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChargeEntity(Guid id)
    {
        var result = await chargeService.DeleteChargeEntityAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}