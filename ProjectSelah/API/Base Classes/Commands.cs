using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSelah.API
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Func<bool> canExecute;

        private Action<object> callback;

        public Command(Action<object> callbackEvent)
        {
            callback = callbackEvent;
        }

        public Command(Action<object> callbackEvent, Func<bool> canExecute)
        {
            callback = callbackEvent;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        public void Execute(object parameter)
        {
            callback?.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
