using System.Text;

namespace AIResumeMatchAnalyzer.Infrastructure.FileProcessing;

public class TxtResumeExtractor : IFileTextExtractor
{
    public bool CanHandle(string fileExtension)
        => fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase);

    public async Task<string> ExtractTextAsync(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream, Encoding.UTF8, leaveOpen: true);
        return await reader.ReadToEndAsync();
    }
}