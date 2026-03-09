using AIResumeMatchAnalyzer.Application.DTOs;

namespace AIResumeMatchAnalyzer.Application.Interfaces;

public interface IResumeAnalysisService
{
    Task<ResumeAnalysisResponseDto> AnalyzeAsync(ResumeAnalysisRequestDto request);
}