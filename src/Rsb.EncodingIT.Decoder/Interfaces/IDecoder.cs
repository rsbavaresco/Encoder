using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Decoder.Interfaces
{
    public interface IDecoder
    {
        byte[] Decode(byte[] content);
    }
}
