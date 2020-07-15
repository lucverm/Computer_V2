using System.ComponentModel;

namespace Computers.ViewModels.Impl
{
    /// <summary>
    /// Cette classe implémente quelques méthodes utiles 
    /// pour notifier les changements de propriétés.
    /// </summary>
    public abstract class ObservableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertiesChanged(params string[] properties)
        {
            foreach (var p in properties) RaisePropertyChanged(p);
        }

        protected void RaisePropertyChanged(string property) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
