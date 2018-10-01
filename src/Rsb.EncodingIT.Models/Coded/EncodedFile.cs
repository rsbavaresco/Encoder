using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rsb.EncodingIT.Models.Coded
{
    public class EncodedFile
    {
        public FileHeader Header { get; private set; }
        public MemoryStream Content { get; private set; }
        public string Path { get; private set; }

        public EncodedFile(FileHeader header, byte[] content)
        {
            Header = header;
            Content = new MemoryStream(content);
        }

        public EncodedFile(FileHeader header, MemoryStream content)
        {
            Header = header;
            Content = content;
        }

        public void SetPath(string path)
        {
            Path = path;
        }
    }
}
