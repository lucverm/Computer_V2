using Computers.Domains;
using Computers.Domains.ExporterFile;
using Computers.Domains.FactoryExporter;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Computers.ViewModels.Impl
{
    public class DetailsViewModel : ObservableViewModel, IDetailsViewModel
    {
        private ITreeNode _current;

        public DetailsViewModel(ITreeNode root, INotifyNodeChanged observable)
        {
            observable.NodeChanged += (o,node) => this.Update(node);
            
            Update(root);

            this.Exporter = new RelayCommand(this.Export);
        }

        private void Update(ITreeNode node)
        {
            _current = node;
            UpdateInfos();
            RaisePropertiesChanged(nameof(Name), nameof(ImagePath), nameof(Infos));
        }

        private void UpdateInfos()
        {
            Infos.Clear();
            foreach (var k in OptionalKeys) Infos.Add(InfoViewModel.From(k, _current[k]));
        }

        private IEnumerable<string> OptionalKeys => _current.Keys.Except(NodeKeys.Keys);

        public string Name => _current[NodeKeys.Name];

        public string ImagePath => _current[NodeKeys.ImageUrl];

        public ObservableCollection<IInfoViewModel> Infos { get; } = new ObservableCollection<IInfoViewModel>();

        //-----------------------------------------------

        public ICommand Exporter { get; private set; }

        public void Export()
        {
            var factory = Factory.Of(FormatFile.PDF);
            factory.Save(_current, @"C:\Users\lverm\Cs-08-2020\exemple.pdf");
        }
    }

    internal class InfoViewModel : IInfoViewModel
    {
        internal static InfoViewModel From(string k, string v) => new InfoViewModel
        {
            Pair = new KeyValuePair<string, string>(k, v)
        };

        private KeyValuePair<string, string> Pair { get; set; }

        public string Icon => $"/Computers;component/Rsc/{Pair.Key}.png";

        public string Key => Pair.Key;

        public string Value => Pair.Value;
    }
}
