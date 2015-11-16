using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeAnalyzer
{   
    public class CSharpFilesList
    {
        private List<CSharpFileInfo> filesInfo;

        public CSharpFilesList()
        {
            filesInfo = new List<CSharpFileInfo>();
        }

        public IEnumerable<CSharpFileInfo> FilesInfo
        {
            get
            {
                return filesInfo;
            }
        }

        public void Add(CSharpFileInfo fileInfo)
        {
            filesInfo.Add(fileInfo);
        }

        public void Sort(IComparer<CSharpFileInfo> comparer)
        {
            filesInfo.Sort(comparer);
        }
    }
}
