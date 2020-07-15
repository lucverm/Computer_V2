using System;
using System.Windows.Input;

namespace Computers.ViewModels.Stubs
{
    /// <summary>
    /// Implémentation bidon d'une commande destinée aux stubs des modèles de vue.
    /// </summary>
    public class NullCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) { }
    }
}
