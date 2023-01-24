using English_Flashcards.Classes;
using English_Flashcards.Infrastructure.Commands;
using English_Flashcards.Models;
using English_Flashcards.Services.Cards;
using English_Flashcards.ViewModels.Base;
using GoogleAPI_Library.Exceptions;
using System.Collections.Specialized;
using System.Windows.Input;

namespace English_Flashcards.ViewModels
{
    internal class MainPageVewModel : ViewModel
    {
        #region Commands
        
        #region RepeatCardCommand
        public ICommand RepeatCardCommand { get; }
        private bool CanRepeatCardCommandExecute(object p) => IsCardCommandCanExecute();
        private void OnRepeatCardCommandExecute(object p)
        {
            Cards.Enqueue(DisplayedCard);
            DisplayedCard.DisplayOptions.ShowAnswer = false;
            DisplayedCard = Cards.Dequeue();

            UserScore.Wrong++;
        }
        #endregion

        #region CardDoneCommand
        public ICommand CardDoneCommand { get; }
        private bool CanCardDoneCommandExecute(object p) => IsCardCommandCanExecute();
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
        #endregion

        #region ShowCardAnswer
        public ICommand ShowCardAnswerCommand { get; }
        private bool CanShowCardAnswerCommandExecute(object p) => IsCardCommandCanExecute();
        private void OnShowCardAnswerCommandExecute(object p)
        {
            DisplayedCard.DisplayOptions.ShowAnswer = true;
        }
        #endregion

        #endregion

        #region Collections
        public static ObservableQueue<Card> Cards { get; set; }
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
            #endregion

            #region Events
            Application.Current.MainPage.Loaded += LoadCards;
            Cards.CollectionChanged += UpdateCount;
            #endregion

            UserScore = new Score();
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
        /// Specifies the number of cards in the queue
        /// </summary>
        private int _CardsCount;

        /// <summary>
        /// Specifies the number of cards in the queue
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

        #region Methods
        /// <summary>
        /// Loading cards when creating an application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoadCards(object sender, EventArgs e)
        {
            IsBusy = true;
            await FillCards(2);
            IsBusy = false;
            DisplayedCard = Cards.Dequeue();
        }

        /// <summary>
        /// Get the Card data from Google Sheet and insert in queue
        /// </summary>
        /// <param name="startRowId">Determines which row should start the selection from the Google spreadsheet</param>
        /// <returns></returns>
        static async Task FillCards(uint startRowId = 2)
        {
            try
            {
                CardService cardService = new CardService();
                var cards = await cardService.GetCards(startRowId);

                await Task.Run(() =>
                {
                    foreach (Card card in cards)
                    {
                        Cards.Enqueue(card);
                    }
                });
            }
            catch (GoogleSheetsException googleEx)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка!", "Возникла ошибка доступа к Google таблице." +
                        "\nПожалуйста проверьте ваше интернет соединение" +
                        $"\n\nТекст ошибка {googleEx.Message}", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка!", "Возникала ошибка при вополнении" +
                    $"\n\n Текст ошибки {ex.Message}", "Ok");
            }
        }

        /// <summary>
        /// Defines a set of checks under which commands can be executed
        /// </summary>
        /// <returns></returns>
        private bool IsCardCommandCanExecute()
        {
            if (IsBusy == false)
            {
                return true;
            }

            if (Cards.Count != 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the number of cards in the view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateCount(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender is ObservableQueue<Card> queue)
            {
                CardsCount = queue.Count;
            }
        }
        #endregion
    }
}
