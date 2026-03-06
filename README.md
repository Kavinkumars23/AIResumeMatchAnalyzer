# AI Resume Match Analyzer (.NET 8)

A Proof of Concept project built with **.NET 8 ASP.NET Core Web API** that analyzes how well a resume matches a job description using an AI API.

## Features

- Upload Resume (PDF / DOCX / TXT)
- Extract text from resume
- Provide job description
- Analyze resume using AI
- Return structured match analysis

Example Response:

```json
{
 "matchScore": 78,
 "matchedSkills": ["C#", ".NET Core", "REST APIs"],
 "missingSkills": ["Azure", "Docker"],
 "strengths": [
  "Strong backend experience",
  "Good API development background"
 ],
 "improvements": [
  "Add cloud platform experience",
  "Highlight CI/CD knowledge"
 ],
 "finalVerdict": "Good match but resume could be improved for this role."
}
