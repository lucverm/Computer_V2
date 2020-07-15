using Computers.Domains.ExporterFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computers.Domains.FactoryExporter
{
    public class ExportNull : IExporterFile
    {
        public void Save(ITreeNode node, string path)
        {
            
        }
    }
}
