using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Models.Source
{
    public class SourceFile
    {
        public string Path { get; private set; }
        public byte[] Content { get; private set; }
        public string Extension { get; private set; }

        public SourceFile(string path, byte[] content)
        {
            Path = path;
            Content = content;
            ReadExtension();
        }

        private void ReadExtension()
        {
            var index = Path.LastIndexOf('.') + 1;
            Extension = Path.Substring(index, Path.Length - index);
        }
    }
}
