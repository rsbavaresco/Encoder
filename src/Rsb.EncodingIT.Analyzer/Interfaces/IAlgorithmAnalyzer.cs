using Rsb.EncodingIT.Models.Source;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Analyzer.Interfaces
{
    public interface IAlgorithmAnalyzer
    {
        bool Analyze(SourceFile file);
    }
}
