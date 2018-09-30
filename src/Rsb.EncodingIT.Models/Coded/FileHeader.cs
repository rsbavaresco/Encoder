using Rsb.EncodingIT.Pool.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Models.Coded
{
    public class FileHeader
    {
        public AlgorithmPipeline Pipeline { get; private set; }
        public byte[] HuffmanMetadata { get; private set; }
        public string SourceExtension { get; private set; }

        public FileHeader(AlgorithmPipeline pipeline, byte[] huffmanMetadata, string sourceExtension)
        {
            Pipeline = pipeline;
            HuffmanMetadata = huffmanMetadata ?? throw new ArgumentNullException(nameof(huffmanMetadata));
            SourceExtension = sourceExtension;
        }
    }
}
