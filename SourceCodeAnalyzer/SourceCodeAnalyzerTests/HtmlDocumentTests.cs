using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using SourceCodeAnalyzer;
using Should;
using System.Collections.Generic;

namespace SourceCodeAnalyzerTests
{
    [TestClass]
    public class HtmlDocumentTests
    {
        [TestMethod]
        public void HtmlContentSouldBeCorrect()
        {
            string fileName = "file1.cs";
            string file = @"int result = 0;
            for (ushort index = 1; index <= tableSize; index++)
            {
                result += index * index;
            }";

            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            List<CSharpFileInfo> results = new List<CSharpFileInfo>();
            CSharpFileInfo result = new CSharpFileInfo();            
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (CSharpFile csharpFile = new CSharpFile(stream))
                {
                    csharpFile.Analyze();
                    result.Name = fileName;
                    result.LineCount = csharpFile.GetCodeLinesCount();
                    result.CodeRatio = csharpFile.GetCommentLinesCodeRatio();
                }
            }
            string expectedHtml = @"<!DOCTYPE HTML>
<html>
	<style>
		table, th, td { border: 1px solid black; }
	</style><body>
		<table>
			<tr>
				<th>Name</th><th>LineCount</th><th>CodeRatio</th>
			</tr><tr>
				<th>file1.cs</th><th>5</th><th>0:5</th>
			</tr>
		</table>
	</body>
</html>";
            results.Add(result);
            HtmlDocument htmlWriter = new HtmlDocument();
            var htmlContent = htmlWriter.Generate(results);
            htmlContent.ShouldEqual(expectedHtml);
        }
    }
}
