using Rsb.EncodingIT.Decoder.Interfaces;
using Rsb.EncodingIT.Models.Coded;
using Rsb.EncodingIT.Models.Source;
using Rsb.EncodingIT.Pool.LZW;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Decoder.Pipelines
{
    public class LZW_HuffmanPipeline : IPipelineRunner
    {
        public SourceFile Run(EncodedFile encoded, string outputPath)
        {
            var lzwDecoder = new LZWDecompress();
            var huffmanDecoder = new HuffmanDecoder(encoded.Header.HuffmanMetadata);

            var bytes = encoded.Content.ToArray();
            var decoded = huffmanDecoder.Decode(bytes);
            var input = Encoding.ASCII.GetString(decoded);
            var outLzw = lzwDecoder.Decompress(input);

            return new SourceFile(outputPath, outLzw, encoded.Header.SourceExtension);
        }
    }
}
