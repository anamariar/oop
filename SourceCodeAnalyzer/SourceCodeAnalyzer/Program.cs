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
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify the path of the C# file to be analyzed.");
                Environment.Exit(0);
            }
            string filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The specified file was not found. Please provide a valid path for the file.");
                Environment.Exit(0);
            }

            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            CSharpFile file = new CSharpFile(fileStream);
            Console.WriteLine(String.Format("The file has {0} lines.", file.GetCodeLinesCount()));
        }
    }
}
