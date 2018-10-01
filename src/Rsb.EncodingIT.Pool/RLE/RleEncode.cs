using Rsb.EncodingIT.Pool.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rsb.EncodingIT.Pool.RLE
{
    public class RleEncode
    {
        public string Encode(string input)
        {
            //abort
            if (input.Contains("*#")) throw new RleException();

            var sb = new StringBuilder();
            var count = 1;
            var current = input[0];
            for (int i = 1; i < input.Length; i++)
            {
                if (current == input[i])
                {
                    count++;
                }
                else
                {
                    if (count > 4)
                        sb.AppendFormat("*#{0}{1}", count, current);
                    else
                    {
                        if (count > 1)
                        {
                            for(int j = 0; j < count; j++)
                                sb.Append(current);
                        }
                        else
                        {
                            sb.Append(current);
                        }
                    }

                    count = 1;
                    current = input[i];
                }
            }
            sb.AppendFormat("*#{0}{1}", count, current);
            return sb.ToString();
        }
    }
}
