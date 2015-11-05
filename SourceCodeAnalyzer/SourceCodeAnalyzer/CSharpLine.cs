using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeAnalyzer
{
    public enum LineType
    {
        Comment,
        Code,
        Empty
    }

    public class CSharpLine
    {
        private readonly string content;
        private readonly char[] trimChars = { ' ', '\t' };
        private readonly string blockCommentStart = @"/*";
        private readonly string blockCommentEnd = @"*/";
        private readonly string lineCommentStart = @"//";
        
        public CSharpLine(string csharpLine)
        {
            content = csharpLine.Trim(trimChars);
        }        

        public bool IsEndBlockComment()
        {
            return content.Contains(blockCommentEnd) && !content.Contains(blockCommentStart);
        }

        public bool IsStartBlockComment()
        {
            return content.Contains(blockCommentStart) && !content.Contains(blockCommentEnd);
        }

        public bool IsComment()
        {
            return content.StartsWith(lineCommentStart) || (content.StartsWith(blockCommentStart) && content.EndsWith(blockCommentEnd));
        }

        public bool IsOnlyStartBlockComment()
        {
            return content.StartsWith(blockCommentStart) && !content.EndsWith(blockCommentEnd) && !content.Contains(blockCommentEnd);
        }

        public bool IsOnlyEndBlockComment()
        {
            return content.EndsWith(blockCommentEnd) && !content.StartsWith(blockCommentStart) && !content.Contains(blockCommentStart);
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(content);
        }

        public LineType GetType(ref bool isInsideComment)
        {
            if (IsEmpty())
                return LineType.Empty;
            if (IsComment())
                return LineType.Comment;
            if (IsOnlyStartBlockComment()) {
                isInsideComment = true;
                return LineType.Comment;
            }
            if (IsStartBlockComment()) {
                isInsideComment = true;
                return LineType.Code;
            }
            if (IsOnlyEndBlockComment()) {
                isInsideComment = false;
                return LineType.Comment;
            }
            if (IsEndBlockComment()) {
                isInsideComment = false;
                return LineType.Code;
            }
            if (isInsideComment)
                return LineType.Comment;
            else
                return LineType.Code;
        }
    }
}
