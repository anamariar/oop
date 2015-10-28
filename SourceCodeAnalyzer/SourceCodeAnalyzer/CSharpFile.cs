using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SourceCodeAnalyzer
{
    public class CSharpFile
    {
        private List<string> lines;

        public int LinesCount
        {
            get
            {
                return lines.Count();
            }
        }

        public CSharpFile(Stream stream)
        {
            lines = new List<string>();
            using (StreamReader streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    lines.Add(streamReader.ReadLine());
                }
            }
        }
        
    }
}
