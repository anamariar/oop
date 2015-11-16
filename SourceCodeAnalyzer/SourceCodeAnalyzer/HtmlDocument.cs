using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;

namespace SourceCodeAnalyzer
{
    public class HtmlDocument:IDocument
    {        
        public string Generate(CSharpFilesList fileInfoList)
        {
            string content = string.Empty;
            StringBuilder stringBuilder = new StringBuilder("<!DOCTYPE HTML>"+Environment.NewLine);
            using (StringWriter stringWriter = new StringWriter(stringBuilder)) {
                using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter)) {
                    writer.RenderBeginTag(HtmlTextWriterTag.Html);
                    writer.RenderBeginTag(HtmlTextWriterTag.Style);
                    writer.Write(@"table, th, td { border: 1px solid black; }");
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Body);
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    var fields = typeof(CSharpFileInfo).GetFields();
                    foreach(var field in fields) {
                        writer.RenderBeginTag(HtmlTextWriterTag.Th);
                        writer.Write(field.Name);
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();
                    foreach (var fileInfo in fileInfoList.FilesInfo) {
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        foreach (var field in fields) {
                            writer.RenderBeginTag(HtmlTextWriterTag.Th);
                            writer.Write(field.GetValue(fileInfo));
                            writer.RenderEndTag();
                        }
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                content = stringWriter.ToString(); 
            }
            return content;
        }        
        
    }
}
