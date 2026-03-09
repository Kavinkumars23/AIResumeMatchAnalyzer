using AIResumeMatchAnalyzer.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeMatchAnalyzer.Application.Interfaces
{
    public interface IAiAnalysisService
    {
        Task<ResumeAnalysisResponseDto> AnalyzeResumeAsync(string resumeText, string jobDescription);
    }
}
