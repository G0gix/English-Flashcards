using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace English_Flashcards.Classes
{
    internal class ObservableQueue<T> : Queue<T>, INotifyCollectionChanged, IEnumerable<T>, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableQueue() : base() { }

        public ObservableQueue(IEnumerable<T> collection) : base(collection)
        {

        }

        #region Methods

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            if (CollectionChanged != null)
                CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add, item));
        }

        public new T Dequeue()
        {
            var item = base.Dequeue();
            if (CollectionChanged != null)
                CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove, item));
            return item;
        }
        #endregion

        #region Properties

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

            var handlers = PropertyChanged;
            if (handlers is null) return;
        }
        #endregion


        public new IEnumerator<T> GetEnumerator()
        {
            return base.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
