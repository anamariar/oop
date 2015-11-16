using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;
using SourceCodeAnalyzer;
using Should;
using System.Collections.Generic;

namespace SourceCodeAnalyzerTests
{
    [TestClass]
    public class CSharpProjectFileTests
    {
        [TestMethod]
        public void GetFilesShouldReturnCorrectFiles()
        {
            string file = 
            @"<?xml version=""1.0"" encoding=""utf-8""?>
                <Project ToolsVersion = ""14.0"" DefaultTargets = ""Build"" xmlns = ""http://schemas.microsoft.com/developer/msbuild/2003"">     
                    <ItemGroup>
                        <Reference Include = ""System""/>
                    </ItemGroup>
                    <Choose>
                        <Otherwise>
                            <ItemGroup>
                                <Reference Include = ""Microsoft.VisualStudio.QualityTools.UnitTestFramework""/>
                            </ItemGroup>
                        </Otherwise>
                    </Choose>
                    <ItemGroup>
                        <Compile Include = ""folder1\Class1.cs""/>
                        <Compile Include = ""folder1\test2.cs""/>
                        <Compile Include = ""UnitTest1.cs""/>
                        <Compile Include = ""Properties\AssemblyInfo.cs""/>
                    </ItemGroup>
                </Project>";

            List<string> expectedFiles = new List<string>
            {
                @"folder1\Class1.cs",
                @"folder1\test2.cs",
                @"UnitTest1.cs",
                @"Properties\AssemblyInfo.cs"
            };
            byte[] byteArray = Encoding.Default.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                List<string> resultFiles = new List<string>();
                CSharpProjectFile csharpProject = new CSharpProjectFile(stream);
                resultFiles = csharpProject.GetFiles();
                resultFiles.ShouldEqual(expectedFiles); 
            }
        }
    }
}
