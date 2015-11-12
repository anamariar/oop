using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeAnalyzer
{
    public interface IDocument
    {
        string Generate(List<CSharpFileInfo> fileInfoList);
    }
}
