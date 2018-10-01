using Rsb.EncodingIT.Analyzer.Algorithms;
using Rsb.EncodingIT.Analyzer.Interfaces;
using Rsb.EncodingIT.Data;
using Rsb.EncodingIT.Encoder;
using Rsb.EncodingIT.Encoder.Interfaces;
using Rsb.EncodingIT.Encoder.Pipelines;
using Rsb.EncodingIT.Models.Coded;
using Rsb.EncodingIT.Pool.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Analyzer.Bootstrap
{
    public class EncoderAnalyzer : IFileAnalyzer
    { 
        public void Analyze(string path)
        {
            var reader = new FileReader();
            var writer = new FileWriter();

            var source = reader.ReadSource(path);

            var rleAnalyzer = new RLEAnalyzer();
            var shouldUseRle = rleAnalyzer.Analyze(source);

            var encoded = default(EncodedFile);
            var pipeline = default(IPipelineRunner);

            if (shouldUseRle)
                pipeline = new RLE_HuffmanPipeline();                
            else
                pipeline = new LZW_HuffmanPipeline();

            try
            {
                encoded = pipeline.Run(source);
            }
            catch (RleException)
            {
                pipeline = new LZW_HuffmanPipeline();
                encoded = pipeline.Run(source);
            }

            encoded.SetPath(path);

            writer.WriteEncoded(encoded);
        }
    }
}
