using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EndPlate
{
    public class RelayCommand : ICommand
    {
        private readonly Action<Object> _execute;
        private readonly Func<Object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter); 
        }

        void ICommand.Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
