using English_Flashcards.Models.Base;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;

namespace English_Flashcards.Models
{
    internal class Card : Model
    {
        public string EnglishText { get; set; }
        public string RussianText { get; set; }

        public CartDisplayOptions DisplayOptions{ get; set; }

    }

    class CartDisplayOptions : Model
    {
       public Thickness Margin { get; set; }
       public int ZIndex { get; set; }
       public Color BackColor { get; set; }

        #region bool - ShowAnswer 
        /// <summary>
        /// 
        /// </summary>
        private bool _ShowAnswer;

		/// <summary>
		/// 
		/// </summary>
		public bool ShowAnswer
		{
			get { return _ShowAnswer; }
			set => Set(ref _ShowAnswer, value);
		}
		#endregion

	}
}
