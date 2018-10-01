using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Pool.LZW
{
    public class LZWCompress
    {
        public IList<int> Compress(string input)
        {
            // build the dictionary
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            for (int i = 0; i < 256; i++)
                dictionary.Add(((char)i).ToString(), i);

            string w = string.Empty;
            List<int> compressed = new List<int>();

            foreach (char c in input)
            {
                string wc = w + c;
                if (dictionary.ContainsKey(wc))
                {
                    w = wc;
                }
                else
                {
                    // write w to output
                    compressed.Add(dictionary[w]);
                    // wc is a new sequence; add it to the dictionary
                    dictionary.Add(wc, dictionary.Count);
                    w = c.ToString();
                }
            }

            // write remaining output if necessary
            if (!string.IsNullOrEmpty(w))
                compressed.Add(dictionary[w]);

            return compressed;
        }
    }
}
