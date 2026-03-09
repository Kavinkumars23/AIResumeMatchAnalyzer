using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeMatchAnalyzer.Application.Interfaces
{
    public interface IResumeTextExtractor
    {
        Task<string> ExtractTextAsync(string filename, Stream fileStream);
    }
}
