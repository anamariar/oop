using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeAnalyzer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                DisplayHelp();
                Environment.Exit(0);
            }

            string parameter = args[0];
            switch (parameter)
            {
                case "-file":
                    {
                        string filePath = args[1];
                        AnalyzeFile(filePath);
                        break;
                    };
                case "-project":
                    {
                        string projectLocation = args[1];
                        AnalyzeProject(projectLocation);
                        break;
                    };
                default:
                    DisplayHelp();
                    break;   
            }
        }

        private static void AnalyzeProject(string projectFile)
        {
            if (!File.Exists(projectFile))
            {
                Console.WriteLine("The specified file was not found. Please provide a valid project file.");
                Environment.Exit(0);
            }
            using (FileStream fileStream = new FileStream(projectFile, FileMode.Open))
            {
                CSharpProjectFile cSharpProjectFile = new CSharpProjectFile(fileStream);
                var files = cSharpProjectFile.GetFiles();
                Console.WriteLine();
                string rootPath = Path.GetDirectoryName(projectFile);
                foreach (var file in files) {
                    AnalyzeFile(Path.Combine(rootPath, file));
                } 
            }
        }

        private static void AnalyzeFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The specified file was not found. Please provide a valid path for the file.");
                Environment.Exit(0);
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                var fileName = Path.GetFileName(filePath);
                using (CSharpFile file = new CSharpFile(fileStream))
                {
                    file.Analyze();
                    Console.WriteLine(String.Format("{0}\n\n\t\tNumber of code lines: {1}\n\t\tComment/lines code ratio: {2}\n",
                        fileName, file.GetCodeLinesCount(), file.GetCommentLinesCodeRatio()));
                }
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine(@"
            Usage:   
                SourceCodeAnalyzer.exe -file <csharp file path>
                SourceCodeAnalyzer.exe -project <project location> 
                ");
        }
    }
}
