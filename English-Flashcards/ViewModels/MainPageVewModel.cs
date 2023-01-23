using English_Flashcards.Infrastructure.Commands;
using English_Flashcards.Models;
using English_Flashcards.Services;
using English_Flashcards.ViewModels.Base;
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
                await App.Current.MainPage.DisplayAlert("Внимание", "Карты закончились\n\n" +
                    "Ваш счет" +
                    $"\nПравильно: {UserScore.Correct}" +
                    $"\nНе правильно: {UserScore.Wrong}", "Ok");

                SheetRowId += 10;
                await FillCards();
            }

            UserScore.Correct++;
            DisplayedCard = Cards.Dequeue();
        }


        #region ShowCardAnswer
        public ICommand ShowCardAnswerCommand { get; }
        private bool CanShowCardAnswerCommandExecute(object p) => true;
        private async void OnShowCardAnswerCommandExecute(object p)
        {
            DisplayedCard.DisplayOptions.ShowAnswer = true;
        }
        #endregion

        #endregion
        #endregion

        private static GoogleSheetService GoogleSheetService;

        #region Collections
        public  static Queue<Card> Cards { get; set; }
        
        
        #endregion

        public MainPageVewModel()
        {
            #region Commands
            RepeatCardCommand = new LamdaCommand(OnRepeatCardCommandExecute, CanRepeatCardCommandExecute);
            CardDoneCommand = new LamdaCommand(OnCardDoneCommandExecute, CanCardDoneCommandExecute);
            ShowCardAnswerCommand = new LamdaCommand(OnShowCardAnswerCommandExecute, CanShowCardAnswerCommandExecute);
            #endregion

            #region Collections
            
            Task.Run(async () => { 
                await CreateGoogleSheetService();
                await FillCards();
            }).Wait();

            UserScore = new Score();
            DisplayedCard = Cards.Dequeue();
            #endregion
        }

        #region Properties
        #region Card - DisplayedCard 
        /// <summary>
        /// 
        /// </summary>
        private Card _DisplayedCard;

        /// <summary>
        /// 
        /// </summary>
        public Card DisplayedCard
        {
            get { return _DisplayedCard; }
            set => Set(ref _DisplayedCard, value);
        }
        #endregion

        public static int SheetRowId { get; set; } = 10;

        #region Score - UserScore 
        /// <summary>
        /// 
        /// </summary>
        private Score _UserScore;

        /// <summary>
        /// 
        /// </summary>
        public Score UserScore
        {
            get { return _UserScore; }
            set => Set(ref _UserScore, value);
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

        static async Task CreateGoogleSheetService()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("GoogleSheetsSecret.json");
            GoogleSheetService = new GoogleSheetService(stream);
        }

        static async Task FillCards()
        {
            var Data =  await GoogleSheetService.GetCards(SheetRowId);
            Cards = new Queue<Card>(Data);
        }
    }
}
