﻿using Rsb.EncodingIT.Encoder.Interfaces;
using Rsb.EncodingIT.Models.Coded;
using Rsb.EncodingIT.Models.Source;
using Rsb.EncodingIT.Pool.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Encoder.Pipelines
{
    public class RLE_HuffmanPipeline : IPipelineRunner
    {
        public readonly AlgorithmPipeline Pipeline = AlgorithmPipeline.RLE_Huffman;

        public EncodedFile Run(SourceFile source)
        {
            var rleEncoder = new RleEncoder();
            var huffmanEncoder = new HuffmanEncoder();

            var outRle = rleEncoder.Encode(source.Content);                        
            var outHuffman = huffmanEncoder.Encode(outRle);

            var header = new FileHeader(Pipeline, huffmanEncoder.HuffmanMetadata, source.Extension);
            var encodedFile = new EncodedFile(header, outHuffman);

            return encodedFile;
        }
    }
}
