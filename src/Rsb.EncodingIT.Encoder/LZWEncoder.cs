using Rsb.EncodingIT.Encoder.Interfaces;
using Rsb.EncodingIT.Pool.LZW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rsb.EncodingIT.Encoder
{
    public class LZWEncoder : IEncoder
    {
        public byte[] Encode(byte[] content)
        {
            var lzw = new LZWCompress();
            var input = Encoding.ASCII.GetString(content);
            var output = lzw.Compress(input);
            var joined = string.Join(" ", output.Select(n => n));

            return Encoding.ASCII.GetBytes(joined);
        }
    }
}
