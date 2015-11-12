using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;

namespace SourceCodeAnalyzer
{
    public class HtmlDocument:IDocument
    {        
        public string Generate(List<CSharpFileInfo> fileInfoList)
        {
            string content = string.Empty;
            StringBuilder stringBuilder = new StringBuilder("<!DOCTYPE HTML>"+Environment.NewLine);
            using (StringWriter stringWriter = new StringWriter(stringBuilder)) {
                using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter)) {
                    writer.RenderBeginTag(HtmlTextWriterTag.Html);
                    writer.RenderBeginTag(HtmlTextWriterTag.Body);
                    foreach (var fileInfo in fileInfoList) {
                        var fields = typeof(CSharpFileInfo).GetFields();
                        foreach (var field in fields) {
                            writer.RenderBeginTag(HtmlTextWriterTag.P);
                            writer.RenderBeginTag(HtmlTextWriterTag.B);
                            writer.Write(field.Name + ":");
                            writer.RenderEndTag();
                            writer.Write(field.GetValue(fileInfo));
                            writer.RenderEndTag();
                        }
                        writer.RenderBeginTag(HtmlTextWriterTag.P);
                        writer.Write("--------------------------------------------------------------");
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                content = stringWriter.ToString(); 
            }
            return content;
        }        
        
    }
}
