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
        private Stream stream;
        
        public CSharpFile(Stream csharpFileStream)
        {
            stream = csharpFileStream;            
        }

        public uint GetCodeLinesCount()
        {
            return CalculateCodeLinesCount();
        }

        private uint CalculateCodeLinesCount()
        {
            uint lineCount = 0;
            using (StreamReader streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    CSharpLine line = new CSharpLine(streamReader.ReadLine());
                    if (!line.IsLineComment() && !line.IsBlockComment() && !line.IsEmpty())
                    {
                        if (line.IsStartBlockComment())
                        {
                            if (!line.IsOnlyStartBlockComment()) lineCount++;
                            do
                            {
                                if (!streamReader.EndOfStream)
                                {
                                    line = new CSharpLine(streamReader.ReadLine());
                                }
                            } while (!line.IsEndBlockComment());
                            if (!line.IsOnlyEndBlockComment()) lineCount++;
                        }
                        else lineCount++;
                    }
                }
            }
            return lineCount;
        }        

    }
}
