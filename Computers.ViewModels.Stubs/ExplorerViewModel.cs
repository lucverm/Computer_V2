using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Computers.ViewModels.Stubs
{
    public class ExplorerViewModel : IExplorerViewModel
    {
        public string Name => "Processeurs\\Socket 1151";

        public ICommand GoBackCommand => new NullCommand();

        public ObservableCollection<INodeViewModel> Children => new ObservableCollection<INodeViewModel>()
        {
            new NodeViewModelStub() {ImagePath="http://www.hfinformatique.be/images/produits/19684.png", Name="Intel Core i3-9100F", Desc="Intel"},
            new NodeViewModelStub() {ImagePath="http://www.hfinformatique.be/images/produits/19248.jpg", Name="Intel Core i5-9400F", Desc="Intel"},
            new NodeViewModelStub() {ImagePath="http://www.hfinformatique.be/images/produits/17609.jpg", Name="Intel Core i7-8700k", Desc="Intel"},
            new NodeViewModelStub() {ImagePath="http://www.hfinformatique.be/images/produits/18894.jpg", Name="Intel Core i7-9700k", Desc="Intel"},
        };
    }

    internal class NodeViewModelStub : INodeViewModel
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public ICommand Select => new NullCommand();
    }
}