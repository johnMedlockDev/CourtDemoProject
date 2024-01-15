using CourtDemoProject.CaseManagementSystem.Api.Services;
using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CourtDemoProject.CaseManagementSystem.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class CaseParticipantsController(CaseParticipantService caseParticipantService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CaseParticipantDto>>> GetCaseParticipants()
    {
        var caseParticipants = await caseParticipantService.GetCaseParticipantsAsync();
        return Ok(caseParticipants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CaseParticipantDto>> GetCaseParticipantEntity(Guid id)
    {
        var caseParticipantDto = await caseParticipantService.GetCaseParticipantEntityAsync(id);
        return caseParticipantDto == null ? (ActionResult<CaseParticipantDto>) NotFound() : (ActionResult<CaseParticipantDto>) Ok(caseParticipantDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCaseParticipantEntity(Guid id, CaseParticipantDto caseParticipantDto)
    {
        if (id != caseParticipantDto.CaseParticipantEntityId)
        {
            return BadRequest();
        }

        var result = await caseParticipantService.UpdateCaseParticipantEntityAsync(caseParticipantDto);
        return !result ? NotFound() : NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CaseParticipantDto>> PostCaseParticipantEntity(CaseParticipantDto caseParticipantDto)
    {
        var createdDto = await caseParticipantService.AddCaseParticipantEntityAsync(caseParticipantDto);
        return CreatedAtAction(nameof(GetCaseParticipantEntity), new { id = createdDto.CaseParticipantEntityId }, createdDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCaseParticipantEntity(Guid id)
    {
        var result = await caseParticipantService.DeleteCaseParticipantEntityAsync(id);
        return !result ? NotFound() : NoContent();
    }
}