using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCodeAnalyzer;
using Should;

namespace SourceCodeAnalyzerTests
{
    [TestClass]
    public class CSharpLineTests
    {
        [TestMethod]
        public void EmptyLineShouldBeEmptyLine()
        {
            string line = @"        ";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsEmpty().ShouldBeTrue();
        }

        [TestMethod]
        public void LineWithOnlyCommentShouldBeLineComment()
        {
            string line = @"//comment";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsLineComment().ShouldBeTrue();
        }

        [TestMethod]
        public void LineWithCodeAndLineCommentShouldntBeLineComment()
        {
            string line = @"int a = 0;//comment";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsLineComment().ShouldBeFalse();
        }

        [TestMethod]
        public void OnlyBlockCommentShouldBeBlockComment()
        {
            string line = @"/*block comment*/";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsBlockComment().ShouldBeTrue();
        }

        [TestMethod]
        public void LineWithCodeAndBlockCommentShouldntBeBlockComment()
        {
            string line = @"int a = 0;/*block comment*/";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsBlockComment().ShouldBeFalse();
        }

        [TestMethod]
        public void LineWithStartBlockCommentShouldBeStartBlockComment()
        {
            string line = @"result += index * index;/*another";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsStartBlockComment().ShouldBeTrue();
        }

        [TestMethod]
        public void LineWithStartBlockCommentAndCodeShouldntBeOnlyStartBlockComment()
        {
            string line = @"result += index * index;/*another";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsOnlyStartBlockComment().ShouldBeFalse();
        }

        [TestMethod]
        public void LineWithCodeAndBlockCommentShouldntBeStartBlockComment()
        {
            string line = @"int a = 0;/*block comment*/";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsStartBlockComment().ShouldBeFalse();
        }

        [TestMethod]
        public void LineWithEndBlockCommentShouldBeEndBlockComment()
        {
            string line = @"block comment*/";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsEndBlockComment().ShouldBeTrue();
        }

        [TestMethod]
        public void LineWithOnlyEndBlockCommentShouldBeOnlyEndBlockComment()
        {
            string line = @"block comment*/";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsOnlyEndBlockComment().ShouldBeTrue();
        }

        [TestMethod]
        public void LineWithEndBlockCommentAndCodeShouldntBeOnlyEndBlockComment()
        {
            string line = @"on multiple lines*/ result++;";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsOnlyEndBlockComment().ShouldBeFalse();
        }
    }
}
