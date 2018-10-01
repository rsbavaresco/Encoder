using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rsb.EncodingIT.Pool.RLE
{
    public class RleDecode
    {
        public string Decode(string input)
        {
            var temporaryDigit = "";
            var count = 0;
            var sb = new StringBuilder();
            var current = char.MinValue;
            bool foundSequence = false;

            int i = 0;
            while (i < input.Length)
            {
                current = input[i];

                if (current == '*' && ((i + 1) < input.Length))
                {
                    if (input[i + 1] == '#')
                    {
                        foundSequence = true;
                        i = i + 2; continue;
                    }
                }

                if (foundSequence && char.IsDigit(current))
                    temporaryDigit += current;
                else
                {
                    if (temporaryDigit == "")
                        sb.Append(current);
                    else
                    {
                        count = int.Parse(temporaryDigit);
                        temporaryDigit = "";
                        foundSequence = false;
                        for (int j = 0; j < count; j++)
                            sb.Append(current);
                    }
                }
                i = i + 1;
            }
            return sb.ToString();
        }
    }
}
