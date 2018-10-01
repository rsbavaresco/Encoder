using Rsb.EncodingIT.Encoder.Interfaces;
using Rsb.EncodingIT.Pool.Huffman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Rsb.EncodingIT.Encoder
{
    public class HuffmanEncoder : IEncoder
    {
        private HuffmanTree _tree;

        public byte[] HuffmanMetadata { get; private set; }


        public HuffmanEncoder()
        {
            _tree = new HuffmanTree();
        }

        public byte[] Encode(byte[] content)
        {
            var encoding = Encoding.ASCII.GetString(content);            
            var huffmanOutput = Encode(encoding);
            SerializeMetadata();

            return huffmanOutput;
        }

        private void SerializeMetadata()
        {
            var memory = new MemoryStream();
            var binaryWriter = new BinaryFormatter();
            var bits = BitConverter.GetBytes(_tree.BitCountForTree);
            memory.Write(bits, 0, bits.Length);            

            binaryWriter.Serialize(memory, _tree.Frequencies.FrequencyTable);
            memory.Position = 0;            
            HuffmanMetadata = memory.ToArray();

            memory.Close();
        }

        private byte[] Encode(string input)
        {
            _tree.BuildTree(input); //Build the huffman tree

            BitArray encoded = _tree.Encode(input); //Encode the tree

            //First show the generated binary output
            //Console.WriteLine(string.Join(string.Empty, encoded.Cast<bool>().Select(bit => bit ? "1" : "0")));

            //Next, convert the binary output to the new characterized output string.       
            byte[] bytes = new byte[(encoded.Length / 8) + 1];
            encoded.CopyTo(bytes, 0);

            //string inText = Encoding.ASCII.GetString(bytes);
            //Console.WriteLine(inText); //Write the compressed output to the textbox.
            return bytes;
        }
    }
}
