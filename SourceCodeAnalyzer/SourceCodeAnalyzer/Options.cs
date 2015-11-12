using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeAnalyzer
{
    public class Options
    {
        [Option('f', "file", Required = false, HelpText = "Input csharp file to analyze.")]
        public string InputFile { get; set; }

        [Option('p', "project", Required = false, HelpText = "Input csharp project to analyze.")]
        public string InputProject { get; set; }

        [Option('e', "export", Required = false, HelpText = "Export file type. E.g: html")]
        public string ExportFileType { get; set; }

        [Option("destPath", Required = false, HelpText = "Full path for exported file. If not specified, the file will be generated in the current directory.")]
        public string ExportFilePath { get; set; }


        [HelpOption(HelpText = "Display this help screen.")]
        public string GetUsage()
        {
            string helpText = HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));

            string helpUsage = @"
      Usage:   
                SourceCodeAnalyzer.exe --file=<csharp file path>
                SourceCodeAnalyzer.exe --project=<project location>    
                SourceCodeAnalyzer.exe --project=<project location> --export=html --destPath=<html file path>           
            ";
            return helpText + helpUsage;
        }
    }
}
