using System;
using System.Windows.Input;

namespace Computers.ViewModels.Impl
{
    public class BasicRelayCommand : ICommand
    {
        private Action _receiver;

        public BasicRelayCommand(Action receiver)
        {
            if (receiver == null) throw new ArgumentNullException("receiver");
            this._receiver = receiver;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _receiver();
    }
}
