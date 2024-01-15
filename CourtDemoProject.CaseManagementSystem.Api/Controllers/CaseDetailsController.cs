using CourtDemoProject.CaseManagementSystem.Api.Services;
using CourtDemoProject.CaseManagementSystem.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CourtDemoProject.CaseManagementSystem.Api.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class CaseDetailsController(CaseDetailService caseDetailService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CaseDetailDto>>> GetCaseDetails()
    {
        return Ok(await caseDetailService.GetCaseDetailsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CaseDetailDto>> GetCaseDetail(Guid id)
    {
        var caseDetailDto = await caseDetailService.GetCaseDetailAsync(id);
        if (caseDetailDto == null)
        {
            return NotFound();
        }
        return caseDetailDto;
    }

    [HttpPost]
    public async Task<ActionResult<CaseDetailDto>> PostCaseDetailEntity(CaseDetailDto caseDetailDto)
    {
        var createdCaseDetail = await caseDetailService.AddCaseDetailAsync(caseDetailDto);
        return CreatedAtAction(nameof(GetCaseDetail), new { id = createdCaseDetail.CaseDetailId }, createdCaseDetail);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCaseDetailEntity(Guid id, CaseDetailDto caseDetailDto)
    {
        if (id != caseDetailDto.CaseDetailId)
        {
            return BadRequest();
        }

        var result = await caseDetailService.UpdateCaseDetailAsync(caseDetailDto);

        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCaseDetailEntity(Guid id)
    {
        try
        {
            await caseDetailService.DeleteCaseDetailAsync(id);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
