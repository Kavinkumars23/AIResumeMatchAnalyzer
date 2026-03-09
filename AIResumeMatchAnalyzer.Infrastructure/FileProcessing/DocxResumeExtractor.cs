using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeMatchAnalyzer.Infrastructure.FileProcessing
{
    public class DocxResumeExtractor : IFileTextExtractor
    {
        public bool CanHandle(string fileExtension)
       => fileExtension.Equals(".docx", StringComparison.OrdinalIgnoreCase);

        public Task<string> ExtractTextAsync(Stream fileStream)
        {
            throw new NotImplementedException("DOCX extraction not implemented yet.");
        }
    }
}
