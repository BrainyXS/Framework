using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Framework.Contract.Navigation;

namespace Framework.Contract
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        public AsyncRelayCommand(Func<Task> execute)
        {
            _execute = execute;
            _canExecute = () => true;
        }

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public async void Execute(object parameter)
        { 
            await _execute();
        }

        public event EventHandler CanExecuteChanged;
    }
}