using Rsb.EncodingIT.Analyzer.Interfaces;
using Rsb.EncodingIT.Data;
using Rsb.EncodingIT.Decoder.Pipelines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Analyzer.Bootstrap
{
    public class DecoderAnalyzer : IFileAnalyzer
    {
        public void Analyze(string path)
        {
            var fileReader = new FileReader();
            var fileWriter = new FileWriter();

            var encodedFile = fileReader.ReadEncoded(path);
            var pipeline = new RLE_HuffmanPipeline();

            var index = path.LastIndexOf('.');
            var outPutPath = path.Remove(index, path.Length - index);
            var source = pipeline.Run(encodedFile, outPutPath);

            fileWriter.WriteSource(source);
        }
    }
}
