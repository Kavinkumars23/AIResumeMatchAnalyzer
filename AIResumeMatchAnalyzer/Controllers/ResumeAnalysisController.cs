using AIResumeMatchAnalyzer.Application.DTOs;
using AIResumeMatchAnalyzer.Application.Interfaces;
using AIResumeMatchAnalyzer.Infrastructure.FileProcessing;
using Microsoft.AspNetCore.Mvc;

namespace AIResumeMatchAnalyzer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResumeAnalysisController : ControllerBase
{
    private readonly IResumeAnalysisService _resumeAnalysisService;

    public ResumeAnalysisController(IResumeAnalysisService resumeAnalysisService)
    {
        _resumeAnalysisService = resumeAnalysisService;
    }

    [HttpPost("analyze")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Analyze([FromForm] ResumeAnalysisRequestDto request)
    {
        try
        {
            if (request.ResumeFile != null)
            {
                FileValidationHelper.Validate(request.ResumeFile.FileName, request.ResumeFile.Length);
            }

            var result = await _resumeAnalysisService.AnalyzeAsync(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (NotSupportedException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An unexpected error occurred.",
                details = ex.Message
            });
        }
    }
}