using English_Flashcards.Infrastructure.Commands;
using English_Flashcards.ViewModels.Base;
using System.Windows.Input;

namespace English_Flashcards.ViewModels
{
    internal class MainPageVewModel : ViewModel
    {

		#region Commands




		#region SwipeCommand
		public ICommand SwipeCommand { get; }
		private bool CanSwipeCommandExecute(object p) => true;
		private async void OnSwipeCommandExecute(object p)
		{
            await Application.Current.MainPage.DisplayAlert("Swipe", p.ToString(), "Ok");
        }
        #endregion
        #endregion



		public MainPageVewModel()
        {
            #region Commands
            SwipeCommand = new LamdaCommand(OnSwipeCommandExecute, CanSwipeCommandExecute);
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
