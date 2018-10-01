using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Encoder.Interfaces
{
    public interface IEncoder
    {
        byte[] Encode(byte[] content);
    }
}
