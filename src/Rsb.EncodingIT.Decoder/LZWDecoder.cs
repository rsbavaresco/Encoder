using Rsb.EncodingIT.Decoder.Interfaces;
using Rsb.EncodingIT.Pool.LZW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rsb.EncodingIT.Decoder
{
    public class LZWDecoder : IDecoder
    {
        public byte[] Decode(byte[] content)
        {
            var lzw = new LZWDecompress();
            var input = Encoding.ASCII.GetString(content);
            var output = lzw.Decompress(input);

            return output;
        }
    }
}
