using Computers.Domains.ExporterFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;

namespace Computers.Domains.FactoryExporter
{
    public class ExportPDF : IExporterFile
    {
        public void Save(ITreeNode node, string path)
        {
            var document = new IronPdf.HtmlToPdf();
            document.RenderHtmlAsPdf(GetContentString(node)).SaveAs(path);
        }

        private string GetContentString(ITreeNode node)
        {
            string title = node.Keys.Contains(NodeKeys.Name) ? node[NodeKeys.Name] : "<h1>Pas de titre</h1>";
            string description = node.Keys.Contains(NodeKeys.Description) ? node[NodeKeys.Description] : "<p>Pas de description</p><br>";
            string imageUrl = node.Keys.Contains(NodeKeys.ImageUrl) ? node[NodeKeys.ImageUrl] : "<p>Pas d'image</p><br>";
            string infos = "";

            foreach(string Key in node.Keys.Except(NodeKeys.Keys))
            {
                infos += $"<li>{Key} : {node[$"{Key}"]}</li>";
            }

            if (!infos.Equals("")) infos = "<h2>Caractéristique : </h2><ol>" + infos + "</ol>";
            

            string content = $"<h1 style='text-align:center'>{title}</h1>" +
                $"<h2> Description : </h2><p>{description}</p>" +
                $"<h2> Illustration : </h2><img width='200px' height='200px' src={imageUrl}>" +
                infos;

            return content;
        }
    }
}