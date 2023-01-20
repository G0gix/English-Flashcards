using System.Windows.Input;

namespace English_Flashcards.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }

}
