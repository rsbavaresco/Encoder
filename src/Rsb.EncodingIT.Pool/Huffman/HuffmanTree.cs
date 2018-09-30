using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rsb.EncodingIT.Pool.Huffman
{
    public sealed class HuffmanTree
    {
        #region Fields
        /// <summary>
        /// Field for keeping the Huffman nodes in. Internally used.
        /// </summary>
        private List<HuffmanNode> _nodes;
        #endregion

        #region Properties

        /// <summary>
        /// Holds the Huffman tree
        /// </summary>
        public HuffmanNode Root { get; set; }

        /// <summary>
        /// Holds the frequency table for all parsed characters
        /// </summary>
        public HuffmanFrequencyTable Frequencies { get; private set; }
 
        /// <summary>
        /// Holds the amount of bits after encoding the tree.
        /// Primary usable for decoding.
        /// </summary>
        public int BitCountForTree { get; private set; } = default(int);

        #endregion

        #region

        public HuffmanTree()
        {
            _nodes = new List<HuffmanNode>();
            Frequencies = new HuffmanFrequencyTable();
        }

        public HuffmanTree(HuffmanFrequencyTable frequencies)
        {
            Frequencies = frequencies;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Builds the Huffman tree
        /// </summary>
        /// <param name="source">The source to build the Hufftree from</param>
        /// <exception cref="ArgumentNullException">Thrown when source is null or empty</exception>
        public void BuildTree(string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException("source");

            Frequencies.Accept(source);
            BuildTree();                
        }

        /// <summary>
        /// Builds the Huffman tree
        /// </summary>                
        public void BuildTree()
        {
            if (!Frequencies.FrequencyTable.Any())
                throw new ArgumentNullException("Frequency table is empty");

            _nodes.Clear(); //As we build a new tree, first make sure it's clean :)

            foreach (KeyValuePair<char, int> symbol in Frequencies.FrequencyTable)
            {
                _nodes.Add(new HuffmanNode() { Character = symbol.Key, Frequency = symbol.Value });
            }

            while (_nodes.Count > 1)
            {
                List<HuffmanNode> orderedNodes = _nodes.OrderBy(node => node.Frequency).ToList();

                if (orderedNodes.Count >= 2)
                {
                    List<HuffmanNode> takenNodes = orderedNodes.Take(2).ToList();

                    HuffmanNode parent = new HuffmanNode()
                    {
                        Character = null,
                        Frequency = takenNodes[0].Frequency + takenNodes[1].Frequency,
                        Left = takenNodes[0],
                        Right = takenNodes[1]
                    };

                    //Remove the childnodes from the original node list and add the new parent node
                    _nodes.Remove(takenNodes[0]);
                    _nodes.Remove(takenNodes[1]);
                    _nodes.Add(parent);
                }
            }

            Root = _nodes.FirstOrDefault();
        }

        /// <summary>
        /// Encodes a given string to the corresponding huffman encoding path
        /// </summary>
        /// <param name="source">The source to encode</param>
        /// <returns>The binary huffman representation of the source</returns>
        public BitArray Encode(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                List<bool> encodedSource = new List<bool>();
                //Traverse the tree for each character in the passed source (string) and add the binary path to the encoded source
                encodedSource.AddRange(source.SelectMany(character =>
                                                            Root.Traverse(character, new List<bool>()))
                                             .ToList());

                //For decoding, we might need the amount of bits to skip trailing bits.
                BitCountForTree = encodedSource.Count;
                return new BitArray(encodedSource.ToArray());
            }
            else return null;
        }

        /// <summary>
        /// Decodes a given binary path to represent it's string value
        /// </summary>
        /// <param name="bits">BitArray for traversing the tree</param>
        /// <returns></returns>
        public string Decode(BitArray bits)
        {
            HuffmanNode current = Root;
            string decodedString = string.Empty;

            foreach (bool bit in bits)
            {
                //Find the correct current node depending on the bit set or not set.
                current = (bit ? current.Right ?? current : current.Left ?? current);

                if (current.IsLeaf())
                {
                    decodedString += current.Character;
                    current = Root;
                }
            }

            return decodedString;
        }

        #endregion
    }
}
