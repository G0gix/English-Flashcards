using English_Flashcards.Infrastructure.Commands;
using English_Flashcards.Models;
using English_Flashcards.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace English_Flashcards.ViewModels
{
    internal class MainPageVewModel : ViewModel
    {

        #region Commands
        #region DeleteCardCommand
        public ICommand DeleteCardCommand { get; }
        private bool CanDeleteCardCommandExecute(object p) => true;
        private async void OnDeleteCardCommandExecute(object p)
        {
            await Application.Current.MainPage.DisplayAlert("Delete", "Delete", "Ok");
        }
        #endregion


        #region CardDone Command
        public ICommand CardDone { get; }
        private bool CanCardDoneExecute(object p) => true;
        private async void OnCardDoneExecute(object p)
        {
            await Application.Current.MainPage.DisplayAlert("Done", "cARD DONE", "Ok");
        }


        #endregion


        #region SwipeCommand
        public ICommand SwipeCommand { get; }
        private bool CanSwipeCommandExecute(object p) => true;
        private async void OnSwipeCommandExecute(object p)
        {
            await Application.Current.MainPage.DisplayAlert("Swipe", p.ToString(), "Ok");
        }
        #endregion
        #endregion

        #region Collections
        public ObservableCollection<Card> Cards;
        #endregion

        public MainPageVewModel()
        {
            #region Commands
            SwipeCommand = new LamdaCommand(OnSwipeCommandExecute, CanSwipeCommandExecute);
            DeleteCardCommand = new LamdaCommand(OnDeleteCardCommandExecute, CanDeleteCardCommandExecute);
            CardDone = new LamdaCommand(OnCardDoneExecute, CanCardDoneExecute);
            #endregion

            #region Collections
            Cards = new ObservableCollection<Card>
            {
                new Card { RussianText = "algorithm", EnglishText = "алгоритм" },
                new Card {EnglishText="among", RussianText = "среди, между" }
            };
            #endregion
        }


        #region Properties
        #region string - Title 
        /// <summary>
        /// Main window Title
        /// </summary>
        private string _Title = "Изучение английского языка";

        /// <summary>
        /// Main Window Title
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set => Set(ref _Title, value);
        }
        #endregion
        #endregion
    }
}
