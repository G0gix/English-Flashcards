using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace English_Flashcards.ViewModels.Base
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

            var handlers = PropertyChanged;
            if (handlers is null) return;
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }


        #region bool - IsBusy 
        /// <summary>
        /// Determines if the application is busy with a long running operation
        /// </summary>
        private bool _IsBusy;

        /// <summary>
        /// Determines if the application is busy with a long running operation
        /// </summary>
        public bool IsBusy
        {
            get { return _IsBusy; }
            set => Set(ref _IsBusy, value);
        }
        #endregion
    }
}
