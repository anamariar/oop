using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeAnalyzer
{
    public class FilenameComparer:IComparer<CSharpFileInfo>
    {
        public int Compare(CSharpFileInfo x, CSharpFileInfo y )
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
