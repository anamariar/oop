using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeAnalyzer
{
    public class LineCountComparer : IComparer<CSharpFileInfo> {

      public int Compare(CSharpFileInfo x, CSharpFileInfo y )
      {
          return x.LineCount.CompareTo(y.LineCount);
      }

   }
}
