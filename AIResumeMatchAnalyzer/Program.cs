using AIResumeMatchAnalyzer.Application.Interfaces;
using AIResumeMatchAnalyzer.Application.Services;
using AIResumeMatchAnalyzer.Infrastructure.AIIntegration;
using AIResumeMatchAnalyzer.Infrastructure.FileProcessing;
using AIResumeMatchAnalyzer.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Swagger and OpenAi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Application Services
builder.Services.AddScoped<IResumeAnalysisService, ResumeAnalysisService>();

builder.Services.AddScoped<IResumeTextExtractor, ResumeTextExtractor>();

builder.Services.AddScoped<IFileTextExtractor, TxtResumeExtractor>();
builder.Services.AddScoped<IFileTextExtractor, PdfResumeExtractor>();
builder.Services.AddScoped<IFileTextExtractor, DocxResumeExtractor>();

//builder.Services.AddScoped<IAiAnalysisService, FakeAiAnalysisService>();

builder.Services.Configure<OpenAiSettings>(
    builder.Configuration.GetSection("OpenAiSettings"));

builder.Services.AddHttpClient<IAiAnalysisService, OpenAiAnalysisService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
