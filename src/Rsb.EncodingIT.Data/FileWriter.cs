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
            var path = sourceFile.Path;
            if (!string.IsNullOrEmpty(sourceFile.Extension))
                path += "." + sourceFile.Extension;

            File.WriteAllBytes(path, sourceFile.Content);           
        }

        public void WriteEncoded(EncodedFile file)
        {
            var entireFile = new HeaderParser().WriteHeader(file.Header, file.Content);

            var path = file.Path;
            var index = path.LastIndexOf('.');
            if (index > -1)
            {
                path = path.Remove(index, path.Length - index);                
            }
            path += ".rsb";

            File.WriteAllBytes(path, entireFile);
        }
    }
}
