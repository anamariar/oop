using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SourceCodeAnalyzer
{
    public class CSharpFile: IDisposable
    {
        private Stream stream;
        private StreamReader streamReader;
        private uint lineCount = 0;
        private uint commentCount = 0;
        private uint emptyCount = 0;
        
        public CSharpFile(Stream csharpFileStream)
        {
            stream = csharpFileStream;
            streamReader = new StreamReader(stream);
            Analyze();
        }              

        private void Analyze() 
        {
            stream.Seek(0, SeekOrigin.Begin);
            bool isInsideComment = false;
            while (!streamReader.EndOfStream) {
                CSharpLine line = new CSharpLine(streamReader.ReadLine());
                switch (line.GetType(ref isInsideComment)) {
                    case LineType.Comment:
                        commentCount++;
                        break;
                    case LineType.Empty:
                        emptyCount++;
                        break;
                    default:
                        lineCount++;
                        break;
                }
            }
        }

        public uint GetCodeLinesCount()
        {            
            return lineCount;
        }

        public string GetCommentLinesCodeRatio() 
        {
            return string.Format("{0}:{1}", commentCount, lineCount);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (streamReader != null) streamReader.Dispose();
            }
        }
        
    }
}
