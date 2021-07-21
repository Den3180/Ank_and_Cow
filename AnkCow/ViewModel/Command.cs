using System;
using System.Windows.Input;

namespace AnkCow
{   
        internal abstract class Command : ICommand
        {

            public event EventHandler CanExecuteChanged;
            protected abstract void Execute();
               
            protected virtual bool CanExecute()
            {

                return true;
            }
        
            protected virtual void OnCanExecuteChanged(EventArgs e)
            {
            CanExecuteChanged?.Invoke(this, e);
          
            }

            public void RaiseCanExecuteChanged()
            {
                OnCanExecuteChanged(EventArgs.Empty);
            }
            bool ICommand.CanExecute(object parameter)
            {
                return CanExecute();
            }

            void ICommand.Execute(object parameter)
            {
                Execute();
            }
        }


    
}
