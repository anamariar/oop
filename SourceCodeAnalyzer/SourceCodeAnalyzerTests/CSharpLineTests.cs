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
        public void LineWithLineCommentShouldBeComment()
        {
            string line = @"//comment";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsComment().ShouldBeTrue();
        }

        [TestMethod]
        public void LineWithCodeAndLineCommentShouldntBeComment()
        {
            string line = @"int a = 0;//comment";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsComment().ShouldBeFalse();
        }

        [TestMethod]
        public void BlockCommentShouldBeComment()
        {
            string line = @"/*block comment*/";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsComment().ShouldBeTrue();
        }

        [TestMethod]
        public void LineWithCodeAndBlockCommentShouldntBeComment()
        {
            string line = @"int a = 0;/*block comment*/";
            CSharpLine csharpLine = new CSharpLine(line);
            csharpLine.IsComment().ShouldBeFalse();
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

        [TestMethod]
        public void LineTypeShouldBeCode() {
            string line = @"for (ushort index = 1; index <= tableSize; index++)/*count this line*/";
            CSharpLine csharpLine = new CSharpLine(line);
            bool isInsideComment = false;
            csharpLine.GetType(ref isInsideComment).ShouldEqual(LineType.Code);
        }

        [TestMethod]
        public void LineTypeShouldBeComment() {
            string line = @"for (ushort index = 1; index <= tableSize; index++)";
            CSharpLine csharpLine = new CSharpLine(line);
            bool isInsideComment = true;
            csharpLine.GetType(ref isInsideComment).ShouldEqual(LineType.Comment);
        }

        [TestMethod]
        public void LineTypeShouldBeEmpty() {
            string line = @"    ";
            CSharpLine csharpLine = new CSharpLine(line);
            bool isInsideComment = true;
            csharpLine.GetType(ref isInsideComment).ShouldEqual(LineType.Empty);
        }

        [TestMethod]
        public void LineTypeForLineCommentShouldBeComment() {
            string line = @"    //comment";
            CSharpLine csharpLine = new CSharpLine(line);
            bool isInsideComment = false;
            csharpLine.GetType(ref isInsideComment).ShouldEqual(LineType.Comment);
        }

        [TestMethod]
        public void LineTypeForStartBlockCommentShouldBeCode() {
            string line = @"int i=0;/*start comment";
            CSharpLine csharpLine = new CSharpLine(line);
            bool isInsideComment = false;
            csharpLine.GetType(ref isInsideComment).ShouldEqual(LineType.Code);
        }

        [TestMethod]
        public void LineTypeForOnlyStartBlockCommentShouldBeComment() {
            string line = @"/*only start comment";
            CSharpLine csharpLine = new CSharpLine(line);
            bool isInsideComment = false;
            csharpLine.GetType(ref isInsideComment).ShouldEqual(LineType.Comment);
        }

        [TestMethod]
        public void LineTypeForEndBlockCommentShouldBeCode() {
            string line = @"end comment/*int i=0;";
            CSharpLine csharpLine = new CSharpLine(line);
            bool isInsideComment = false;
            csharpLine.GetType(ref isInsideComment).ShouldEqual(LineType.Code);
        }

        [TestMethod]
        public void LineTypeForOnlyEndBlockCommentShouldBeComment() {
            string line = @"only end comment*/";
            CSharpLine csharpLine = new CSharpLine(line);
            bool isInsideComment = false;
            csharpLine.GetType(ref isInsideComment).ShouldEqual(LineType.Comment);
        }
    }
}
