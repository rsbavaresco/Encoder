using Rsb.EncodingIT.Models.Coded;
using Rsb.EncodingIT.Models.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rsb.EncodingIT.Data
{
    public class FileWriter
    {
        public void WriteSource(SourceFile sourceFile)
        {
            //var outPath = sourceFile.Path.
        }

        public void WriteEncoded(EncodedFile file, string path)
        {
            var entireFile = new HeaderParser().WriteHeader(file.Header, file.Content);

            File.WriteAllBytes(path, entireFile);
        }
    }
}
