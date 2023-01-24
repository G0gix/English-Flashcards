
/* Unmerged change from project 'English-Flashcards (net6.0-maccatalyst)'
Before:
using System;
using English_Flashcards.Infrastructure.Commands.Base;
using Command = English_Flashcards.Infrastructure.Commands.Base.Command;
After:
using English_Flashcards.Infrastructure.Commands.Base;
using System;
using Command = English_Flashcards.Infrastructure.Commands.Base.Command;
*/

/* Unmerged change from project 'English-Flashcards (net6.0-android)'
Before:
using System;
using English_Flashcards.Infrastructure.Commands.Base;
using Command = English_Flashcards.Infrastructure.Commands.Base.Command;
After:
using English_Flashcards.Infrastructure.Commands.Base;
using System;
using Command = English_Flashcards.Infrastructure.Commands.Base.Command;
*/

/* Unmerged change from project 'English-Flashcards (net6.0-ios)'
Before:
using System;
using English_Flashcards.Infrastructure.Commands.Base;
using Command = English_Flashcards.Infrastructure.Commands.Base.Command;
After:
using English_Flashcards.Infrastructure.Commands.Base;
using System;
using Command = English_Flashcards.Infrastructure.Commands.Base.Command;
*/
using Command = English_Flashcards.Infrastructure.Commands.Base.Command;

namespace English_Flashcards.Infrastructure.Commands
{
    internal class LamdaCommand : Command
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public LamdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            _Execute(parameter);
        }
    }
}
