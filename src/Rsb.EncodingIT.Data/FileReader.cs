using Rsb.EncodingIT.Models.Coded;
using Rsb.EncodingIT.Models.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rsb.EncodingIT.Data
{
    public class FileReader
    {
        public SourceFile ReadSource(string path)
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) throw new FileNotFoundException(path);

            var bytes = File.ReadAllBytes(path);
            return new SourceFile(path, bytes);
        }

        public EncodedFile ReadEncoded(string path)
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) throw new FileNotFoundException(path);

            var bytes = File.ReadAllBytes(path);
            var stream = new MemoryStream(bytes);
            
            var header = new HeaderParser()
                            .ReadHeader(stream, out byte[] content);

            return new EncodedFile(header, content);
        }
    }
}
