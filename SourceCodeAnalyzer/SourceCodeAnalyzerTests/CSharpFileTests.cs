using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using SourceCodeAnalyzer;
using Should;

namespace SourceCodeAnalyzerTests
{
    [TestClass]
    public class CSharpFileTests
    {
        [TestMethod]
        public void CodeLineCountForCsharpFileShouldBe()
        {
            string file = @"int result = 0;
            for (ushort index = 1; index <= tableSize; index++)
            {
                result += index * index;
            }";
            uint expectedCount = 5;
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (CSharpFile csharpFile = new CSharpFile(stream))
                {
                    csharpFile.Analyze();
                    uint lineCount = csharpFile.GetCodeLinesCount();
                    lineCount.ShouldEqual(expectedCount);
                }
            }
        }

        [TestMethod]
        public void CodeLineCountWhenFileHasLineCommentsShouldBe()
        {
            string file = @"int result = 0;
            //shouldn't be counted
            for (ushort index = 1; index <= tableSize; index++)//this line should be counted
            {
                result += index * index;
            }
            //shouldn't be counted";
            uint expectedCount = 5;
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (CSharpFile csharpFile = new CSharpFile(stream))
                {
                    csharpFile.Analyze();
                    uint lineCount = csharpFile.GetCodeLinesCount();
                    lineCount.ShouldEqual(expectedCount);
                }
            }
        }

        [TestMethod]
        public void CodeLineCountWhenFileHasBlockCommentsShouldBe()
        {
            string file = @"int result = 0;
            /*shouldn't be counted
            because it is a comment,
            a block comment*/
            for (ushort index = 1; index <= tableSize; index++)/*count this line*/
            {
                result += index * index;/*another
                comment
                on multiple lines*/ result++;
            }
            /*ana
            are mere*/
            /*this is a comment*/";
            uint expectedCount = 6;
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (CSharpFile csharpFile = new CSharpFile(stream))
                {
                    csharpFile.Analyze();
                    uint lineCount = csharpFile.GetCodeLinesCount();
                    lineCount.ShouldEqual(expectedCount);
                }
            }
        }

        [TestMethod]
        public void CodeLineCountWhenFileHasCodeAndBlockCommentsShouldBe()
        {
            string file = @"int result = 0;            
            for (ushort index = 1; index <= tableSize; index++)/*count this line*/
            {
                result += index * index;/*another
                comment
                on multiple lines*/ result++;
            }
            /*this is a comment*/";
            uint expectedCount = 6;
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (CSharpFile csharpFile = new CSharpFile(stream))
                {
                    csharpFile.Analyze();
                    uint lineCount = csharpFile.GetCodeLinesCount();
                    lineCount.ShouldEqual(expectedCount);
                }
            }
        }

        [TestMethod]
        public void CodeLineCountWhenFileHasCodeAndBlockCommentsShouldBe2()
        {
            string file = @"string operationResult = DoOperation(expression[index], expression[index + 1], expression[index + 2]);/*sdsd
                string[] newExpression = ReplaceExpression(expression, operationResult, index);
                */return Calculator(newPrefixedExpression); ";
            uint expectedCount = 2;
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (CSharpFile csharpFile = new CSharpFile(stream))
                {
                    csharpFile.Analyze();
                    uint lineCount = csharpFile.GetCodeLinesCount();
                    lineCount.ShouldEqual(expectedCount);
                }
            }
        }

        [TestMethod]
        public void CodeLineCountWhenFileHasEmptyLinesShouldBe()
        {
            string file = @"
            
            i++";
            uint expectedCount = 1;
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (CSharpFile csharpFile = new CSharpFile(stream))
                {
                    csharpFile.Analyze();
                    uint lineCount = csharpFile.GetCodeLinesCount();
                    lineCount.ShouldEqual(expectedCount);
                }
            }
        }        

        [TestMethod]
        public void CodeRatioShouldBeCorrect()
        {
            string file = @"//count
            int result = 0;//don't count
            /*should be counted
            because it is a comment,
            a block comment*/
            for (ushort index = 1; index <= tableSize; index++)/*don't count this line*/
            {
                result += index * index;/*another
                comment
                on multiple lines*/ result++;
            }
            /*ana
            are mere*/
            /*this is a comment*/";
            string expectedRatio = "8:6";
            uint expectedCount = 6;
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (CSharpFile csharpFile = new CSharpFile(stream))
                {
                    csharpFile.Analyze();
                    uint lineCount = csharpFile.GetCodeLinesCount();
                    lineCount.ShouldEqual(expectedCount);
                    string codeRatio = csharpFile.GetCommentLinesCodeRatio();
                    codeRatio.ShouldEqual(expectedRatio);
                } 
            }
        }
    }
}
