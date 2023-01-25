namespace English_Flashcards.Services
{
    internal class PlatformIntegration
    {
        internal static async void SpeakText(string TextToBeSpoken)
        {
            IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();

            SpeechOptions options = new SpeechOptions()
            {
                Pitch = 1.0f,   // 0.0 - 2.0
                Volume = 1.0f, // 0.0 - 1.0
                Locale = locales.FirstOrDefault(lang => lang.Country == "USA"),
            };

            await TextToSpeech.Default.SpeakAsync(TextToBeSpoken, options);
        }
    }
}
