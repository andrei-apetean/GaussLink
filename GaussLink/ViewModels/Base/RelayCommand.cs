using System;
using System.Windows.Input;

namespace GaussLink.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        private readonly Action mAction;

        public RelayCommand(Action action)
        {
            mAction = action;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
