using Rsb.EncodingIT.Models.Coded;
using Rsb.EncodingIT.Models.Source;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Decoder.Interfaces
{
    public interface IPipelineRunner
    {
        SourceFile Run(EncodedFile source, string outputPath);
    }
}
