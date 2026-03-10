using AIResumeMatchAnalyzer.Application.DTOs;
using AIResumeMatchAnalyzer.Application.Interfaces;

namespace AIResumeMatchAnalyzer.Application.Services;

public class ResumeAnalysisService : IResumeAnalysisService
{
    private readonly IResumeTextExtractor _resumeTextExtractor;
    private readonly IAiAnalysisService _aiAnalysisService;

    public ResumeAnalysisService(
        IResumeTextExtractor resumeTextExtractor,
        IAiAnalysisService aiAnalysisService)
    {
        _resumeTextExtractor = resumeTextExtractor;
        _aiAnalysisService = aiAnalysisService;
    }

    public async Task<ResumeAnalysisResponseDto> AnalyzeAsync(ResumeAnalysisRequestDto request)
    {
        if (request.ResumeFile == null)
            throw new ArgumentException("Resume file is required.");

        if (string.IsNullOrWhiteSpace(request.JobDescription))
            throw new ArgumentException("Job description is required.");


        await using var stream = request.ResumeFile.OpenReadStream();

        var resumeText = await _resumeTextExtractor.ExtractTextAsync(
            request.ResumeFile.FileName,
            stream);

        if (string.IsNullOrWhiteSpace(resumeText))
            throw new InvalidOperationException("Could not extract text from the uploaded resume.");

        return await _aiAnalysisService.AnalyzeResumeAsync(resumeText, request.JobDescription);
    }
}