using Rsb.EncodingIT.Decoder.Interfaces;
using Rsb.EncodingIT.Pool.Huffman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Rsb.EncodingIT.Decoder
{
    public class HuffmanDecoder : IDecoder
    {
        private readonly byte[] _huffmanMetadata;
        private HuffmanTree _tree;

        public HuffmanDecoder(byte[] huffmanMetadata)
        {
            _huffmanMetadata = huffmanMetadata;
        }

        public byte[] Decode(byte[] content)
        {
            DeserializeHuffmanMetadata();
            var decoded = Decode(new BitArray(content));
            var encodingBack = Encoding.ASCII.GetBytes(decoded);
            return encodingBack;
        }

        private void DeserializeHuffmanMetadata()
        {            
            var memory = new MemoryStream(_huffmanMetadata);
            var formatter = new BinaryFormatter();
            var reader = new BinaryReader(memory);            

            var bits = reader.ReadInt32();

            var huffmanFrequencyTable = (Dictionary<char, int>) formatter.Deserialize(memory);
            _tree = new HuffmanTree(new HuffmanFrequencyTable(huffmanFrequencyTable), bits);

            reader.Close();
            reader.Dispose();
            memory.Close();            
        }

        private string Decode(BitArray bytes)
        {
            //First convert the compressed output to a bit array again again and skip trailing bits.            
            //var bytes = Encoding.ASCII.GetBytes(input);
            _tree.BuildTree();

            var boolAr = bytes.Cast<bool>().Take(_tree.BitCountForTree).ToArray();

            var binary = new BitArray(boolAr);

            return _tree.Decode(binary);            
        }
    }
}
