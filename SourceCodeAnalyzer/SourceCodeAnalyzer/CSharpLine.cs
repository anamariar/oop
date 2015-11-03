using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeAnalyzer
{
    public class CSharpLine
    {
        private string content;
        private char[] trimChars = new char[] { ' ', '\t' };
        private string blockCommentStart = @"/*";
        private string blockCommentEnd = @"*/";
        private string lineCommentStart = @"//";
        
        public CSharpLine(string csharpLine)
        {
            content = csharpLine.Trim(trimChars);
        }

        public bool IsEndBlockComment()
        {
            return content.Contains(blockCommentEnd) && !content.StartsWith(blockCommentStart);
        }

        public bool IsStartBlockComment()
        {
            return content.Contains(blockCommentStart) && !content.EndsWith(blockCommentEnd);
        }

        public bool IsComment()
        {
            return content.StartsWith(lineCommentStart) || (content.StartsWith(blockCommentStart) && content.EndsWith(blockCommentEnd));
        }

        public bool IsOnlyStartBlockComment()
        {
            return content.StartsWith(blockCommentStart) && !content.EndsWith(blockCommentEnd);
        }

        public bool IsOnlyEndBlockComment()
        {
            return content.EndsWith(blockCommentEnd) && !content.StartsWith(blockCommentStart);
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(content);
        }
    }
}
