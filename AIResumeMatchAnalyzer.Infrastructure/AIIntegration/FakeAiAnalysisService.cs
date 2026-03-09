using AIResumeMatchAnalyzer.Application.DTOs;
using AIResumeMatchAnalyzer.Application.Interfaces;

namespace AIResumeMatchAnalyzer.Infrastructure.AIIntegration;

public class FakeAiAnalysisService : IAiAnalysisService
{
    public Task<ResumeAnalysisResponseDto> AnalyzeResumeAsync(string resumeText, string jobDescription)
    {
        var result = new ResumeAnalysisResponseDto
        {
            MatchScore = 75,
            MatchedSkills = new List<string>
            {
                "C#",
                ".NET",
                "REST APIs"
            },
            MissingSkills = new List<string>
            {
                "Azure",
                "Docker"
            },
            Strengths = new List<string>
            {
                "Strong backend development",
                "Good API design"
            },
            Improvements = new List<string>
            {
                "Add cloud experience",
                "Highlight CI/CD knowledge"
            },
            FinalVerdict = "Good match but resume could be improved."
        };

        return Task.FromResult(result);
    }
}