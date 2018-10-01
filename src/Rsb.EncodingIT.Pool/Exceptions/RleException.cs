using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Pool.Exceptions
{
    public class RleException : Exception
    {
        public RleException() : base("Rle should be aborted")
        {

        }
    }
}
