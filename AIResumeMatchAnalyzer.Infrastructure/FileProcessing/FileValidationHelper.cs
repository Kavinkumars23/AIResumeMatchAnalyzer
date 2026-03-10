namespace AIResumeMatchAnalyzer.Infrastructure.FileProcessing;

public static class FileValidationHelper
{
    private static readonly string[] AllowedExtensions = [".pdf", ".docx", ".txt"];
    private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

    public static void Validate(string fileName, long fileLength)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name is required.");

        if (fileLength <= 0)
            throw new ArgumentException("Uploaded file is empty.");
        
        if (fileLength > MaxFileSize)
        {
            throw new ArgumentException("File size cannot exceed 5MB.");
        }

        var extension = Path.GetExtension(fileName);

        if (string.IsNullOrWhiteSpace(extension) ||
            !AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
        {
            throw new NotSupportedException("Only PDF, DOCX, and TXT files are supported.");
        }
    }
}