using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Pool.Pipeline
{
    public enum AlgorithmPipeline
    {
        Invalid = 0, //2b 00
        RLE_Huffman = 1, //2b 01
        LZW_Huffman = 2 ////2b 10
    }
}
