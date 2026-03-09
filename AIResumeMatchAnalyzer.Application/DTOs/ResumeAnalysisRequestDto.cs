using Microsoft.AspNetCore.Http;

namespace AIResumeMatchAnalyzer.Application.DTOs;

public class ResumeAnalysisRequestDto
{
    public IFormFile ResumeFile { get; set; } = default!;

    public string JobDescription { get; set; } = string.Empty;
}