using Computers.Domains;
using Computers.Domains.ExporterFile;
using Computers.Domains.FactoryExporter;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Computers.ViewModels.Impl
{
    public class SaveViewModel : ISaveViewModel
    {
        private ITreeNode _current;

        public ICommand Exporter { get; private set; }

        public SaveViewModel(ITreeNode current)
        {
            this.Exporter = new RelayCommand(this.Export);
            this._current = current;
        }

        public void Export()
        {
            var factory = Factory.Of(FormatFile.PDF);           
            factory.Save(_current, @"C:\Users\lverm\Cs-08-2020\exemple.pdf");
        }
    }
}
