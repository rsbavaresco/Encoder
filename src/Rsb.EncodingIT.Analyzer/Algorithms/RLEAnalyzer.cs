using Rsb.EncodingIT.Analyzer.Interfaces;
using Rsb.EncodingIT.Models.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rsb.EncodingIT.Analyzer.Algorithms
{
    public class RLEAnalyzer : IAlgorithmAnalyzer
    {
        public bool Analyze(SourceFile file)
        {
            var content = file.Content;
            return Analyze(content);
        }

        public bool Analyze(byte[] bytes)
        {
            var content = Encoding.ASCII.GetString(bytes).ToCharArray();
            var runs = new List<int>();
            var current = default(char);
            var runLenght = 0;

            int i = 1;
            current = content[0];

            while (i < content.Length)
            {
                if (current == content[i])
                    runLenght++;
                else
                {
                    if (runLenght > 4) runs.Add(runLenght);
                    runLenght = 0;
                    current = content[i];
                }
                i++;
            }

            var sum = runs.Sum();
            return sum > bytes.Length * 0.15;
        }
    }
}
