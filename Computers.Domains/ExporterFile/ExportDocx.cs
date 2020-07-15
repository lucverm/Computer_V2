using Computers.Domains.ExporterFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace Computers.Domains.FactoryExporter
{
    public class ExportDocx : IExporterFile
    {
        public void Save(ITreeNode node, string path)
        {          
            var doc = DocX.Create(path);
 
            doc.InsertParagraph("Hello Word");

            doc.Save();
    
            //Process.Start("WINWORD.EXE", path);
        }

        private Stream GetStreamFromUrl(string fromUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(fromUrl);
                return new MemoryStream(data);
            }
        }
    }

}
