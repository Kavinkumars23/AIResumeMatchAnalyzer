using DocumentFormat.OpenXml.Packaging;

namespace AIResumeMatchAnalyzer.Infrastructure.FileProcessing;

public class DocxResumeExtractor : IFileTextExtractor
{
    public bool CanHandle(string fileExtension)
        => fileExtension.Equals(".docx", StringComparison.OrdinalIgnoreCase);

    public Task<string> ExtractTextAsync(Stream fileStream)
    {
        using var document = WordprocessingDocument.Open(fileStream, false);
        var body = document.MainDocumentPart?.Document.Body;

        var text = body?.InnerText ?? string.Empty;

        return Task.FromResult(text);
    }
}