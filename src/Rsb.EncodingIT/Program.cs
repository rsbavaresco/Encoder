using Rsb.EncodingIT.Huffman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Rsb.EncodingIT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int a = 123;
            byte b = 1;

            File.WriteAllBytes(@"C:\temp\teste.txt", BitConverter. GetBytes(a));

            HuffmanTree tree = new HuffmanTree();

            //var bytes = File.ReadAllBytes(@"F:\Unisinos\Teoria da Informação\cantrbry\alice29.txt");
            var bytes = File.ReadAllBytes(@"C:\temp\eclipse.exe");

            //var inText = Encode("The quick brown fox jumps over the lazy programmer", tree);

            var inText = Encode(Encoding.Unicode.GetString(bytes), tree);

            var table = SerializeFrequencies(tree.Frequencies.FrequencyTable);
            
            File.WriteAllBytes(@"C:\temp\eclipse_coded.txt", inText);

            var readedBytes = File.ReadAllBytes(@"C:\temp\eclipse_coded.txt");

            var decoded = Decode(readedBytes, tree);

            var decodedBytes = Encoding.Unicode.GetBytes(decoded);

            File.WriteAllBytes(@"C:\Temp\eclipse_novo.exe", decodedBytes);

            Console.ReadKey();
        }

        private static MemoryStream SerializeFrequencies(Dictionary<char, int> frequencies)
        {
            var memory = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memory, frequencies);

            return memory;
        }

        public static byte[] Encode(string input, HuffmanTree tree)
        {             

            tree.BuildTree(input); //Build the huffman tree

            BitArray encoded = tree.Encode(input); //Encode the tree

            //First show the generated binary output
            Console.WriteLine(string.Join(string.Empty, encoded.Cast<bool>().Select(bit => bit ? "1" : "0")));

            //Next, convert the binary output to the new characterized output string.       
            byte[] bytes = new byte[(encoded.Length / 8) + 1];
            encoded.CopyTo(bytes, 0);

            //string inText = Encoding.ASCII.GetString(bytes);
            //Console.WriteLine(inText); //Write the compressed output to the textbox.
            return bytes;
        }

        public static string Decode(byte[] bytes, HuffmanTree tree)
        {
            //First convert the compressed output to a bit array again again and skip trailing bits.            
            //var bytes = Encoding.ASCII.GetBytes(input);

            bool[] boolAr = new BitArray(bytes).Cast<bool>().Take(tree.BitCountForTree).ToArray();
            BitArray encoded = new BitArray(boolAr);

            string decoded = tree.Decode(encoded);
            //Console.WriteLine("Decoded result: " + decoded);
            return decoded;
        }
    }
}
