using English_Flashcards.Models;
using English_Flashcards.ViewModels;

namespace English_Flashcards;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        MainPageVewModel.CardAnimation += CardAnimation;
    }

    private async void CardAnimation(CardAnimationMode mode)
    {
        double animationСoordinates = 0;

        if (mode == CardAnimationMode.Repeat)
        {
            animationСoordinates = -50;
        }
        else
        {
            animationСoordinates = 50;
        }

        await Task.WhenAny<bool>(
            Card.TranslateTo(animationСoordinates, 0, 250, Easing.Linear),
            Card.FadeTo(0.0, 250, Easing.Linear)
            );

        await Task.Delay(500);
        Card.TranslationX = 0;
        Card.TranslationY = 0;
        Card.Opacity = 1.0;

        await Card.ScaleTo(1.1, 300, Easing.CubicIn);
        await Card.ScaleTo(1.0, 100, Easing.CubicOut);
    }
}

