﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SourceCodeAnalyzer
{
    public class CSharpProjectFile: IDisposable
    {
        private XDocument xmlDoc;
        private Stream stream;

        public CSharpProjectFile(Stream xmlStream)
        {
            stream = xmlStream;
            xmlDoc = XDocument.Load(stream);
        }

        public List<string> GetFiles()
        {
            List<string> files = new List<string>();
            var allNodes = xmlDoc.DescendantNodes();
            foreach (var node in allNodes)
            {
                if (node is XElement)
                {
                    var elem = node as XElement;
                    if (elem.Name.LocalName.Equals("Compile"))
                    {
                        files.Add(elem.FirstAttribute.Value);
                    }
                }
            }
            return files;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ( stream != null) stream.Dispose();
            }
        }
    }
}
