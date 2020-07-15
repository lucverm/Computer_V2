using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computers.Domains.ExporterFile
{
    public interface IExporterFile
    { 
       void Save(ITreeNode node, string path);
    }
}
