using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Computers.ViewModels
{
    public interface IExplorerViewModel
    {
        string Name { get; }
        ICommand GoBackCommand { get; }
        ObservableCollection<INodeViewModel> Children { get; }
    }

    public interface INodeViewModel 
    {
        string ImagePath { get; }
        string Name { get; }
        string Desc { get; }

        ICommand Select { get; }
    }
}