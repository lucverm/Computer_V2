using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Computers.ViewModels
{
    public interface IDetailsViewModel
    {
        string Name { get; }
        string ImagePath { get; }

        ObservableCollection<IInfoViewModel> Infos { get; }
        ICommand Exporter { get; }
        void Export();

    }

    public interface IInfoViewModel
    {
        string Icon { get; }
        string Key { get; }

        string Value { get; }
    }
}