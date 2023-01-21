using Microsoft.Maui;
using Microsoft.Maui.Graphics;

namespace English_Flashcards.Models
{
    internal class Card
    {
        public string EnglishText { get; set; }
        public string RussianText { get; set; }
        public CartDisplayOptions DisplayOptions{ get; set; }

    }

    class CartDisplayOptions
    {
       public Thickness Margin { get; set; }
       public int ZIndex { get; set; }
       public Color BackColor { get; set; }
    }
}
