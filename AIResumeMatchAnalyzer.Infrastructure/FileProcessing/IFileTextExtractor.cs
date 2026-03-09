namespace AIResumeMatchAnalyzer.Infrastructure.FileProcessing;

public interface IFileTextExtractor
{
    bool CanHandle(string fileExtension);
    Task<string> ExtractTextAsync(Stream fileStream);
}