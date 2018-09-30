using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Models.Coded
{
    public class EncodedFile
    {
        public FileHeader Header { get; private set; }
        public byte[] Content { get; private set; }

        public EncodedFile(FileHeader header, byte[] content)
        {
            Header = header;
            Content = content;
        }
    }
}
