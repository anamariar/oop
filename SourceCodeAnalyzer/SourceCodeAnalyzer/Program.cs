using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace SourceCodeAnalyzer
{
    public class Program
    {
        public static CSharpFilesList results = new CSharpFilesList();

        public static void Main(string[] args)
        {
            var options = new Options();
            if (Parser.Default.ParseArguments(args, options))
            {
                if (!string.IsNullOrEmpty(options.InputFile)) {
                    results.Add(AnalyzeFile(options.InputFile));
                } else
                    if (!string.IsNullOrEmpty(options.InputProject)) {
                    AnalyzeProject(options.InputProject);
                } else {
                    Console.WriteLine(options.GetUsage());
                    Environment.Exit(0);
                }
                if ((!string.IsNullOrEmpty(options.SortType))) {
                    IComparer<CSharpFileInfo> comparer = GetComparer(options.SortType);
                    if (comparer != null)
                        results.Sort(comparer);
                    else
                        Console.WriteLine("Invalid comparer specified.");
                }
                WriteToConsole(results);
                if ((!string.IsNullOrEmpty(options.ExportFileType))) {
                    ExportToFile(options.ExportFilePath, options.ExportFileType);
                }
            }
        }

        private static IComparer<CSharpFileInfo> GetComparer(string sortType)
        {
            switch (sortType) {
                case "lineCount":
                    return new LineCountComparer();
                case "name":
                    return new FilenameComparer();
                default:
                    return null;
            }
        }

        private static void ExportToFile(string exportFile, string exportType)
        {
            switch (exportType) {
                case "html":
                    HtmlDocument htmlDoc = new HtmlDocument();
                    string htmlDocContent = htmlDoc.Generate(results);
                    FileHelper exportFileHelper = new FileHelper(exportFile, exportType);
                    exportFileHelper.Write(htmlDocContent);
                    Console.WriteLine("Exported to: " + exportFileHelper.FullPath);
                    break;
                default:
                    Console.WriteLine("The export type you specified is not currently supported.");
                    break;
            }
        }

        private static void AnalyzeProject(string projectFile)
        {
            ValidateFile(projectFile, ".csproj");
            using (FileStream fileStream = new FileStream(projectFile, FileMode.Open))
            {
                CSharpProjectFile cSharpProjectFile = new CSharpProjectFile(fileStream);
                var files = cSharpProjectFile.GetFiles();
                string rootPath = Path.GetDirectoryName(projectFile);
                foreach (var file in files)
                   results.Add(AnalyzeFile(Path.Combine(rootPath, file)));
            }
        }        

        private static CSharpFileInfo AnalyzeFile(string filePath)
        {
            ValidateFile(filePath, ".cs");
            CSharpFileInfo fileResults = new CSharpFileInfo();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (CSharpFile file = new CSharpFile(fileStream))
                {
                    file.Analyze();
                    fileResults.Name = Path.GetFileName(filePath);
                    fileResults.LineCount = file.GetCodeLinesCount();
                    fileResults.CodeRatio = file.GetCommentLinesCodeRatio();
                }
            }
            return fileResults;
        }

        private static void WriteToConsole(CSharpFilesList results)
        {
            foreach (var fileInfo in results.FilesInfo) {
                Console.WriteLine(String.Format("{0}\n\n\t\tNumber of code lines: {1}\n\t\tComment/lines code ratio: {2}\n",
                        fileInfo.Name, fileInfo.LineCount, fileInfo.CodeRatio));
            }            
        }

        private static void ValidateFile(string file, string fileType)
        {
            if (!File.Exists(file)) {
                Console.WriteLine(String.Format("The specified file was not found. Please provide a valid {0} file.", fileType));
                Environment.Exit(0);
            }
            if (Path.GetExtension(file) != fileType) {
                Console.WriteLine(String.Format("The specified file is not a {0} file.", fileType));
                Environment.Exit(0);
            }            
        }
        
    }
}
