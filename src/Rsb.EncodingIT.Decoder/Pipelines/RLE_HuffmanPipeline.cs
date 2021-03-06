﻿using Rsb.EncodingIT.Decoder.Interfaces;
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
            var rleDecoder = new RleDecoder();
            var huffmanDecoder = new HuffmanDecoder(encoded.Header.HuffmanMetadata);

            var bytes = encoded.Content.ToArray();
            var decoded = huffmanDecoder.Decode(bytes);
            var outRle = rleDecoder.Decode(decoded);

            return new SourceFile(outputPath, outRle, encoded.Header.SourceExtension);
        }
    }
}
