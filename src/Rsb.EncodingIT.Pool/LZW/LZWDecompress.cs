using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rsb.EncodingIT.Pool.LZW
{
    public class LZWDecompress
    {
        public byte[] Decompress(string content)
        {
            IList<int> input = content.Split(' ')
                                      .Select(s => Convert.ToInt32(s))
                                      .ToList();
            
            // build the dictionary
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
                dictionary.Add(i, ((char)i).ToString());

            string w = dictionary[input[0]];
            input.RemoveAt(0);
            StringBuilder decompressed = new StringBuilder(w);

            foreach (int k in input)
            {
                string entry = null;
                if (dictionary.ContainsKey(k))
                    entry = dictionary[k];
                else if (k == dictionary.Count)
                    entry = w + w[0];

                decompressed.Append(entry);

                // new sequence; add it to the dictionary
                dictionary.Add(dictionary.Count, w + entry[0]);

                w = entry;
            }

            return Encoding.ASCII.GetBytes(decompressed.ToString());
        }
    }
}
