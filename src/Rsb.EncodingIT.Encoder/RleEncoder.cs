using Rsb.EncodingIT.Encoder.Interfaces;
using Rsb.EncodingIT.Pool.RLE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Encoder
{
    public class RleEncoder : IEncoder
    {
        public byte[] Encode(byte[] content)
        {
            var encoding = Encoding.ASCII.GetString(content);
            var rle = new RleEncode();
            var output = rle.Encode(encoding);

            return Encoding.ASCII.GetBytes(output);
        }
    }
}
