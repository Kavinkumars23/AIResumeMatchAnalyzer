using AIResumeMatchAnalyzer.Application.Interfaces;

namespace AIResumeMatchAnalyzer.Infrastructure.FileProcessing;

public class ResumeTextExtractor : IResumeTextExtractor
{
    private readonly IEnumerable<IFileTextExtractor> _extractors;

    public ResumeTextExtractor(IEnumerable<IFileTextExtractor> extractors)
    {
        _extractors = extractors;
    }

    public async Task<string> ExtractTextAsync(string fileName, Stream fileStream)
    {
        var extension = Path.GetExtension(fileName);

        var extractor = _extractors.FirstOrDefault(x => x.CanHandle(extension));

        if (extractor == null)
            throw new NotSupportedException($"File type '{extension}' is not supported.");

        return await extractor.ExtractTextAsync(fileStream);
    }
}