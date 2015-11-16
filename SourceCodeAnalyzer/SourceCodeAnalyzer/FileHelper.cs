using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeAnalyzer
{
    public class FileHelper
    {
        private string fullPath;
        private const string defaultName = "AnalyzerResults";

        public FileHelper(string path, string fileType)
        {
            fullPath = path;
            if (string.IsNullOrEmpty(fullPath)) {
                fullPath = Directory.GetCurrentDirectory();
                fullPath = Path.Combine(fullPath, defaultName + "." + fileType);
            }             
        }

        public string FullPath
        {
            get { return fullPath;}
        }

        public void Write(string content)
        {        
            File.WriteAllText(fullPath, content);
        }
    }
}
