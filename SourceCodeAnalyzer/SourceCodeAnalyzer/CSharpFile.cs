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
        
        public CSharpFile(Stream csharpFileStream)
        {
            stream = csharpFileStream;
            streamReader = new StreamReader(stream); 
        }              

        public uint GetCodeLinesCount()
        {
            stream.Seek(0, SeekOrigin.Begin);
            uint lineCount = 0;
            while (!streamReader.EndOfStream)
            {
                CSharpLine line = new CSharpLine(streamReader.ReadLine());
                if (!line.IsComment() && !line.IsEmpty())
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
            return lineCount;
        }

        public string GetCommentLinesCodeRatio()
        {
            stream.Seek(0, SeekOrigin.Begin);
            uint commentCount = 0;
            uint lineCount = 0;
            while (!streamReader.EndOfStream)
            {
                CSharpLine line = new CSharpLine(streamReader.ReadLine());
                lineCount++;
                if (!line.IsEmpty())
                {
                    if (line.IsComment())
                    {
                        commentCount++;
                    }
                    else
                    {
                        if (line.IsStartBlockComment())
                        {
                            if (line.IsOnlyStartBlockComment()) commentCount++;
                            while (!line.IsEndBlockComment())
                            {
                                if (!streamReader.EndOfStream)
                                {
                                    line = new CSharpLine(streamReader.ReadLine());
                                    lineCount++;
                                    commentCount++;
                                }
                            }
                            if (!line.IsOnlyEndBlockComment()) commentCount--;
                        }
                    }
                }
            }
            return string.Format("{0}:{1}", commentCount, lineCount );
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
