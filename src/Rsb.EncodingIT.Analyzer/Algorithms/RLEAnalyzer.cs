using Rsb.EncodingIT.Analyzer.Interfaces;
using Rsb.EncodingIT.Models.Source;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Analyzer.Algorithms
{
    public class RLEAnalyzer : IAlgorithmAnalyzer
    {
        public bool Analyze(SourceFile file)
        {
            var content = file.Content;
            return false;

        }

        public int Analyze(byte[] bytes)
        {
            return 0;
        }
    }
}
