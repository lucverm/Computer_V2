using Computers.Domains.ExporterFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computers.Domains.FactoryExporter
{
    public static class Factory
    {
        public static IExporterFile Of (FormatFile format)
        {
            switch (format)
            {
                case FormatFile.Docx:
                    return new ExportDocx();
                case FormatFile.PDF:
                    return new ExportPDF();
                default:
                    return new ExportNull();
            }            
        }
    }
}
