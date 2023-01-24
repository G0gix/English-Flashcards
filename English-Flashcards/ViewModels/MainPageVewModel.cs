using English_Flashcards.Classes;
using English_Flashcards.Infrastructure.Commands;
using English_Flashcards.Models;
using English_Flashcards.Services.Cards;
using English_Flashcards.ViewModels.Base;
using System.Collections.Specialized;
using System.Windows.Input;

namespace English_Flashcards.ViewModels
{
    internal class MainPageVewModel : ViewModel
    {

        #region Commands
        #region RepeatCardCommand
        public ICommand RepeatCardCommand { get; }
        private bool CanRepeatCardCommandExecute(object p) => !IsBusy;
        private async void OnRepeatCardCommandExecute(object p)
        {
            Cards.Enqueue(DisplayedCard);
            DisplayedCard.DisplayOptions.ShowAnswer = false;
            DisplayedCard = Cards.Dequeue();

            UserScore.Wrong++;
        }
        #endregion

        #region CardDoneCommand
        public ICommand CardDoneCommand { get; }
        private bool CanCardDoneCommandExecute(object p) => !IsBusy;
        private async void OnCardDoneCommandExecute(object p)
        {
            if (Cards.Count == 0)
            {
                DisplayedCard = null;
                StartSheetRowId += 10;

                IsBusy = true;
                await FillCards(StartSheetRowId);
                IsBusy = false;

                await App.Current.MainPage.DisplayAlert("Внимание", "Карты закончились\n\n" +
                    "Ваш счет" +
                    $"\nПравильно: {UserScore.Correct}" +
                    $"\nНе правильно: {UserScore.Wrong}", "Ok");
            }

            UserScore.Correct++;
            DisplayedCard = Cards.Dequeue();
        }


        #region ShowCardAnswer
        public ICommand ShowCardAnswerCommand { get; }
        private bool CanShowCardAnswerCommandExecute(object p) => !IsBusy;
        private async void OnShowCardAnswerCommandExecute(object p)
        {
            DisplayedCard.DisplayOptions.ShowAnswer = true;
        }
        #endregion

        #endregion
        #endregion


        #region Collections
        public  static ObservableQueue<Card> Cards { get; set; }
        #endregion

        public MainPageVewModel()
        {
            #region Commands
            RepeatCardCommand = new LamdaCommand(OnRepeatCardCommandExecute, CanRepeatCardCommandExecute);
            CardDoneCommand = new LamdaCommand(OnCardDoneCommandExecute, CanCardDoneCommandExecute);
            ShowCardAnswerCommand = new LamdaCommand(OnShowCardAnswerCommandExecute, CanShowCardAnswerCommandExecute);
            #endregion

            #region Collections
            Cards = new ObservableQueue<Card>();

            Task.Run(async () => { 
                await FillCards();
            }).Wait();

            Cards.CollectionChanged += UpdateCount;

            UserScore = new Score();
            DisplayedCard = Cards.Dequeue();
            
            #endregion
        }

        private void UpdateCount(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender is ObservableQueue<Card> queue)
            {
                CardsCount = queue.Count;
            }
        }

        #region Properties
        #region Card - DisplayedCard 
        /// <summary>
        /// The card to show in the view
        /// </summary>
        private Card _DisplayedCard;

        /// <summary>
        /// The card to show in the view
        /// </summary>
        public Card DisplayedCard
        {
            get { return _DisplayedCard; }
            set => Set(ref _DisplayedCard, value);
        }
        #endregion

        public static uint StartSheetRowId { get; set; } = 0;

        #region Score - UserScore 
        /// <summary>
        /// User account during the knowledge test
        /// </summary>
        private Score _UserScore;

        /// <summary>
        /// User account during the knowledge test
        /// </summary>
        public Score UserScore
        {
            get { return _UserScore; }
            set => Set(ref _UserScore, value);
        }
        #endregion


        #region int - CardsCount 
        /// <summary>
        /// 
        /// </summary>
        private int _CardsCount;

        /// <summary>
        /// 
        /// </summary>
        public int CardsCount
        {
            get { return _CardsCount + 1; }
            set => Set(ref _CardsCount, value);
        }
        #endregion


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

        static async Task FillCards(uint startRowId = 2)
        {
            CardService cardService = new CardService();
            var Data = await cardService.GetCards(startRowId);

            foreach (var item in Data)
            {
                Cards.Enqueue(item);
            }
        }
    }
}
