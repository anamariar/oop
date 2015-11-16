using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCodeAnalyzer;
using Should;
using System.Collections;

namespace SourceCodeAnalyzerTests
{
    [TestClass]
    public class CSharpFilesListTests
    {
        [TestMethod]
        public void AddMethodShouldAddCorrectly()
        {
            CSharpFilesList filesList = new CSharpFilesList();
            CSharpFileInfo fileInfo = new CSharpFileInfo {
                Name = "file1.cs",
                LineCount = 29,
                CodeRatio = "3:6"
            };
            filesList.Add(fileInfo);
            IEnumerator enumerator = filesList.FilesInfo.GetEnumerator();
            enumerator.MoveNext();
            enumerator.Current.ShouldEqual(fileInfo);
        }

        [TestMethod]
        public void SortByLineCountShouldWorkCorrectly()
        {
            CSharpFilesList filesList = new CSharpFilesList();
            CSharpFileInfo fileInfo1 = new CSharpFileInfo {
                Name = "file1.cs",
                LineCount = 29,
                CodeRatio = "3:6"
            };
            CSharpFileInfo fileInfo2 = new CSharpFileInfo {
                Name = "file1.cs",
                LineCount = 15,
                CodeRatio = "3:6"
            };
            CSharpFileInfo fileInfo3 = new CSharpFileInfo {
                Name = "file1.cs",
                LineCount = 59,
                CodeRatio = "3:6"
            };
            filesList.Add(fileInfo1);
            filesList.Add(fileInfo2);
            filesList.Add(fileInfo3);
            filesList.Sort(new LineCountComparer());
            IEnumerator enumerator = filesList.FilesInfo.GetEnumerator();
            enumerator.MoveNext();
            enumerator.Current.ShouldEqual(fileInfo2);
            enumerator.MoveNext();
            enumerator.Current.ShouldEqual(fileInfo1);
            enumerator.MoveNext();
            enumerator.Current.ShouldEqual(fileInfo3);
        }

        [TestMethod]
        public void SortByFilenameShouldWorkCorrectly()
        {
            CSharpFilesList filesList = new CSharpFilesList();
            CSharpFileInfo fileInfo1 = new CSharpFileInfo {
                Name = "z_file1.cs",
                LineCount = 29,
                CodeRatio = "3:6"
            };
            CSharpFileInfo fileInfo2 = new CSharpFileInfo {
                Name = "a_file1.cs",
                LineCount = 115,
                CodeRatio = "3:6"
            };
            CSharpFileInfo fileInfo3 = new CSharpFileInfo {
                Name = "m_file1.cs",
                LineCount = 59,
                CodeRatio = "3:6"
            };
            filesList.Add(fileInfo1);
            filesList.Add(fileInfo2);
            filesList.Add(fileInfo3);
            filesList.Sort(new FilenameComparer());
            IEnumerator enumerator = filesList.FilesInfo.GetEnumerator();
            enumerator.MoveNext();
            enumerator.Current.ShouldEqual(fileInfo2);
            enumerator.MoveNext();
            enumerator.Current.ShouldEqual(fileInfo3);
            enumerator.MoveNext();
            enumerator.Current.ShouldEqual(fileInfo1);
        }
    }
}
