using System;
using System.Windows.Input;

namespace ELL.Desktop.UI.ViewModels.Base
{
    public class SimpleCommand : ICommand
    {
        private Action<object> commandAction;
        private readonly Func<object, bool> cannExectueFunc;

        public event EventHandler CanExecuteChanged;
        public SimpleCommand(Action<object> commandAction, Func<object, bool> cannExectueFunc = null)
        {
            this.commandAction = commandAction;
            this.cannExectueFunc = cannExectueFunc != null
                ? cannExectueFunc
                : (arg) => true;
        }
        public bool CanExecute(object parameter) => 
            cannExectueFunc.Invoke(parameter);

        public void Execute(object parameter) =>
            commandAction.Invoke(parameter);

        public void RaiseCanExecuteChange()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
