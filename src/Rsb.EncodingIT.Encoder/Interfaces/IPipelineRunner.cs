using Rsb.EncodingIT.Models.Coded;
using Rsb.EncodingIT.Models.Source;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rsb.EncodingIT.Encoder.Interfaces
{
    public interface IPipelineRunner
    {
        EncodedFile Run(SourceFile source);
    }
}
