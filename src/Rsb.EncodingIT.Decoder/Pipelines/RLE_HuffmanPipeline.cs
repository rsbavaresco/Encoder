using Rsb.EncodingIT.Decoder.Interfaces;
using Rsb.EncodingIT.Models.Coded;
using Rsb.EncodingIT.Models.Source;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Decoder.Pipelines
{
    public class RLE_HuffmanPipeline : IPipelineRunner
    {
        public SourceFile Run(EncodedFile encoded, string outputPath)
        {
            var huffmanDecoder = new HuffmanDecoder(encoded.Header.HuffmanMetadata);
            var bytes = encoded.Content.ToArray();
            var decoded = huffmanDecoder.Decode(bytes);

            return new SourceFile(outputPath, decoded, encoded.Header.SourceExtension);
        }
    }
}
