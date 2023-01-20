using English_Flashcards.Infrastructure.Commands;
using English_Flashcards.ViewModels.Base;
using System.Windows.Input;

namespace English_Flashcards.ViewModels
{
    internal class MainPageVewModel : ViewModel
    {

		#region Counter
		public ICommand Counter { get; }
		private bool CanCounterExecute(object p) => true;
		private void OnCounterExecute(object p)
		{
			ClickCount++;

        }

		
		#endregion


		public MainPageVewModel()
        {
            Counter = new LamdaCommand(OnCounterExecute, CanCounterExecute);
        }



		#region string - Test 
		/// <summary>
		/// 
		/// </summary>
		private string _Test = "Test";

		/// <summary>
		/// 
		/// </summary>
		public string Test
		{
			get { return _Test; }
			set => Set(ref _Test, value);
		}
		#endregion



		#region int - ClickCount 
		/// <summary>
		/// 
		/// </summary>
		private int _ClickCount;

		/// <summary>
		/// 
		/// </summary>
		public int ClickCount
		{
			get { return _ClickCount; }
			set => Set(ref _ClickCount, value);
		}
		#endregion




	}
}
