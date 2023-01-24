using English_Flashcards.Models.Base;

namespace English_Flashcards.Models
{
    internal class Card : Model
    {
        public string EnglishText { get; set; }
        public string RussianText { get; set; }
        public CartDisplayOptions DisplayOptions { get; set; }

    }

    class CartDisplayOptions : Model
    {
        public Color BackColor { get; set; }

        #region bool - ShowAnswer 
        /// <summary>
        /// Determines if the correct answer is visible
        /// </summary>
        private bool _ShowAnswer;

        /// <summary>
        /// Determines if the correct answer is visible
        /// </summary>
        public bool ShowAnswer
        {
            get { return _ShowAnswer; }
            set => Set(ref _ShowAnswer, value);
        }
        #endregion
    }
}
