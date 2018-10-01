using Rsb.EncodingIT.Analyzer.Algorithms;
using Rsb.EncodingIT.Analyzer.Interfaces;
using Rsb.EncodingIT.Data;
using Rsb.EncodingIT.Encoder;
using Rsb.EncodingIT.Encoder.Pipelines;
using Rsb.EncodingIT.Models.Coded;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Analyzer.Bootstrap
{
    public class EncoderAnalyzer : IFileAnalyzer
    {
        public EncoderAnalyzer()
        {

        }

        public void Analyze(string path)
        {
            var reader = new FileReader();
            var writer = new FileWriter();

            var source = reader.ReadSource(path);

            var rleAnalyzer = new RLEAnalyzer();
            var rle = rleAnalyzer.Analyze(source);
            
            var pipeline = new RLE_HuffmanPipeline();
            var encoded = pipeline.Run(source);
            encoded.SetPath(path);

            writer.WriteEncoded(encoded);
        }


    }
}
