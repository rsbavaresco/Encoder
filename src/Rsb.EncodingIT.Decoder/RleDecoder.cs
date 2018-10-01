using Rsb.EncodingIT.Decoder.Interfaces;
using Rsb.EncodingIT.Pool.RLE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Decoder
{
    public class RleDecoder : IDecoder
    {
        public byte[] Decode(byte[] content)
        {
            var encoding = Encoding.ASCII.GetString(content);
            var rle = new RleDecode();
            var output = rle.Decode(encoding);

            return Encoding.ASCII.GetBytes(output);
        }
    }
}
