using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SourceCodeAnalyzer.Tests
{
    [TestClass]
    public class CSharpFileTests
    {
        [TestMethod]
        public void GetLineCountTest()
        {
            string file = @"int result = 0;
            for (ushort index = 1; index <= tableSize; index++)
            {
                result += index * index;
            }";
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            MemoryStream stream = new MemoryStream(byteArray);
            CSharpFile csharpFile = new CSharpFile(stream);
            Assert.AreEqual(5, csharpFile.LinesCount);
        }

        [TestMethod]
        public void GetLineCountForEmptyFileTest()
        {
            string file = @"";
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            MemoryStream stream = new MemoryStream(byteArray);
            CSharpFile csharpFile = new CSharpFile(stream);
            Assert.AreEqual(0, csharpFile.LinesCount);
        }

    }
}
