using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PRSplineWPF.ViewModel
{
    public class RelayCommand<TParam> : ICommand where TParam : class
    {
        private readonly Action _action;
        private readonly Func<TParam, bool> _func;
        public RelayCommand(Action action)
        {
            _action = action;
        }
        public RelayCommand(Action execute, Func<TParam, bool> canExecute)
        {
            _func = canExecute;
            _action = execute;
        }

        public bool CanExecute(object parameter) => _func == null || _func(parameter as TParam);

        public void Execute(object parameter) => _action();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
