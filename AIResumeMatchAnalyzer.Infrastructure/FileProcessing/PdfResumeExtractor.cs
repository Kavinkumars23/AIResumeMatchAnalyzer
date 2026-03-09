using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeMatchAnalyzer.Infrastructure.FileProcessing
{
    public class PdfResumeExtractor : IFileTextExtractor
    {
        public bool CanHandle(string fileExtension)
       => fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase);

        public Task<string> ExtractTextAsync(Stream fileStream)
        {
            throw new NotImplementedException("PDF extraction not implemented yet.");
        }
    }
}
