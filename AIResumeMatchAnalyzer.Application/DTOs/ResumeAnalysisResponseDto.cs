namespace AIResumeMatchAnalyzer.Application.DTOs;

public class ResumeAnalysisResponseDto
{
    public int MatchScore { get; set; }

    public List<string> MatchedSkills { get; set; } = new();

    public List<string> MissingSkills { get; set; } = new();

    public List<string> Strengths { get; set; } = new();

    public List<string> Improvements { get; set; } = new();

    public string FinalVerdict { get; set; } = string.Empty;
}