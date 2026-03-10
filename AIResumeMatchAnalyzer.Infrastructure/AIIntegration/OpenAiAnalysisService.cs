using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AIResumeMatchAnalyzer.Application.DTOs;
using AIResumeMatchAnalyzer.Application.Interfaces;
using AIResumeMatchAnalyzer.Configuration;
using Microsoft.Extensions.Options;

namespace AIResumeMatchAnalyzer.Infrastructure.AIIntegration;

public class OpenAiAnalysisService : IAiAnalysisService
{
    private readonly HttpClient _httpClient;
    private readonly OpenAiSettings _settings;

    public OpenAiAnalysisService(HttpClient httpClient, IOptions<OpenAiSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task<ResumeAnalysisResponseDto> AnalyzeResumeAsync(string resumeText, string jobDescription)
    {
        var systemPrompt = """
        You are an expert ATS-style resume analyzer and hiring assistant.

        Compare the candidate resume with the job description and return only valid JSON.

        Rules:
        1. matchScore must be an integer between 0 and 100.
        2. Keep arrays concise and relevant.
        3. Return only JSON.
        4. Do not include markdown or extra explanation.
        """;

        var userPrompt = $@"
Analyze the following resume against the given job description.

Resume Text:
{resumeText}

Job Description:
{jobDescription}

Return JSON in this exact structure:
{{
  ""matchScore"": 0,
  ""matchedSkills"": [],
  ""missingSkills"": [],
  ""strengths"": [],
  ""improvements"": [],
  ""finalVerdict"": """"
}}";


        var requestBody = new
        {
            model = _settings.Model,
            temperature = 0.2,
            response_format = new
            {
                type = "json_object"
            },
            messages = new object[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt }
            }
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, _settings.Endpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

        request.Content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json");

        using var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException($"OpenAI API error: {response.StatusCode} - {responseContent}");
        }

        using var jsonDoc = JsonDocument.Parse(responseContent);

        var content = jsonDoc
            .RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        if (string.IsNullOrWhiteSpace(content))
            throw new ApplicationException("AI returned empty content.");

        ResumeAnalysisResponseDto? result;

        try
        {
            result = JsonSerializer.Deserialize<ResumeAnalysisResponseDto>(
                content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
        catch (JsonException)
        {
            throw new ApplicationException("AI returned invalid JSON format.");
        }

        if (result == null)
        {
            throw new ApplicationException("AI response could not be parsed.");
        }

        if (result == null)
            throw new ApplicationException("Failed to parse AI response.");

        return result;
    }
}