
using CourtDemoProject.CaseManagementSystem.Api.Services;
using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CourtDemoProject.CaseManagementSystem.Api.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class CasesController(CaseService caseService) : ControllerBase
{
    // GET: api/CaseEntities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CaseDto>>> GetCases()
    {
        var cases = await caseService.GetCasesAsync();
        return Ok(cases);
    }

    // GET: api/CaseEntities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CaseDto>> GetCaseEntity(string id)
    {
        var caseDto = await caseService.GetCaseEntityAsync(id);
        if (caseDto == null)
        {
            return NotFound();
        }
        return Ok(caseDto);
    }

    // PUT: api/CaseEntities/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCaseEntity(string id, CaseDto caseDto)
    {
        if (id != caseDto.CaseId)
        {
            return BadRequest();
        }

        var result = await caseService.UpdateCaseEntityAsync(caseDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    // POST: api/CaseEntities
    [HttpPost]
    public async Task<ActionResult<CaseDto>> PostCaseEntity(CaseDto caseDto)
    {
        var createdDto = await caseService.AddCaseEntityAsync(caseDto);
        return CreatedAtAction(nameof(GetCaseEntity), new { id = createdDto.CaseId }, createdDto);
    }

    // DELETE: api/CaseEntities/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCaseEntity(string id)
    {
        var result = await caseService.DeleteCaseEntityAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

}