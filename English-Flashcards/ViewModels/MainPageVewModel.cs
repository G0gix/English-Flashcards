using English_Flashcards.Infrastructure.Commands;
using English_Flashcards.Models;
using English_Flashcards.ViewModels.Base;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace English_Flashcards.ViewModels
{
    internal class MainPageVewModel : ViewModel
    {

        #region Commands

        #region RepeatCardCommand
        public ICommand RepeatCardCommand { get; }
        private bool CanRepeatCardCommandExecute(object p) => true;
        private void OnRepeatCardCommandExecute(object p)
        {

        }
        #endregion

        #region CardDoneCommand
        public ICommand CardDoneCommand { get; }
        private bool CanCardDoneCommandExecute(object p) => true;
        private async void OnCardDoneCommandExecute(object p)
        {
            await Application.Current.MainPage.DisplayAlert("Done", "cARD DONE", "Ok");
        }


        #endregion
        #endregion

        #region Collections
        public  static ObservableCollection<Card> Cards { get; set; }
        #endregion

        public MainPageVewModel()
        {
            #region Commands
            RepeatCardCommand = new LamdaCommand(OnRepeatCardCommandExecute, CanRepeatCardCommandExecute);
            CardDoneCommand = new LamdaCommand(OnCardDoneCommandExecute, CanCardDoneCommandExecute);
            #endregion

            #region Collections
            Cards = new ObservableCollection<Card>()
            {
                new Card { RussianText = "algorithm", EnglishText = "алгоритм",
                    DisplayOptions = new CartDisplayOptions
                    {
                        Margin = new Thickness {Left = 10, Top = 0, Right = 0, Bottom = 0,},
                        ZIndex = 1,
                        BackColor = Color.FromHex("#FFFF")
                    }
                },
                new Card {EnglishText="among", RussianText = "среди, между", 
                    DisplayOptions = new CartDisplayOptions
                    { 
                        Margin = new Thickness {Left = 30, Top = 30, Right = 0, Bottom = 0, },
                        ZIndex = 2,
                        BackColor = Color.FromHex("#F95F62"),
                    } 
                },
            };

            DipsplayCard = Cards[1];
            #endregion
        }


        #region Card - DipsplayCard 
        /// <summary>
        /// 
        /// </summary>
        private Card _DipsplayCard;

        /// <summary>
        /// 
        /// </summary>
        public Card DipsplayCard
        {
            get { return _DipsplayCard; }
            set => Set(ref _DipsplayCard, value);
        }
        #endregion


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
