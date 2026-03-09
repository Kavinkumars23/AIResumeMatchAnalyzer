using UglyToad.PdfPig;

namespace AIResumeMatchAnalyzer.Infrastructure.FileProcessing;

public class PdfResumeExtractor : IFileTextExtractor
{
    public bool CanHandle(string fileExtension)
        => fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase);

    public Task<string> ExtractTextAsync(Stream fileStream)
    {
        using var document = PdfDocument.Open(fileStream);

        var text = string.Empty;

        foreach (var page in document.GetPages())
        {
            text += page.Text;
        }

        return Task.FromResult(text);
    }
}