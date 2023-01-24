using Google.Apis.Sheets.v4;
using GoogleAPI_Library.Models;
using GoogleAPI_Library;
using English_Flashcards.Models;

namespace English_Flashcards.Services.Cards
{
    class CardService
    {
        private readonly GoogleSheetsManager googleSheetsManager;
        internal static uint SelectionStep { get; set; } = 10;
        private static uint Step = SelectionStep;

        #region Colors
        private static List<string> ColorsList = new List<string>
        {
            "#8B6969",
            "#DF9D9D",
            "#838EDE",
            "#A8ACFF",
            "#5959AB",
            "#3B3178",
            "#9370DB",
            "#5E2D79",
            "#8B668B",
            "#38B0DE",
            "#5F9F9F",
        };
        #endregion

        public CardService()
        {
            Stream SecretStream = null;

            Task.Run(async () =>
            {
                SecretStream = await FileSystem.OpenAppPackageFileAsync("GoogleSheetsSecret.json");
            }).Wait();

            GoogleCredentialOptions_Stream googleCredential = new GoogleCredentialOptions_Stream
            {
                ApplicationName = "Dictionary-Translator",
                Scopes = new string[] { SheetsService.Scope.Spreadsheets },
                ClientSecretStream = SecretStream
            };

            googleSheetsManager = new GoogleSheetsManager(googleCredential);
        }

        internal async Task<IEnumerable<Card>> GetCards(uint startRowId = 2)
        {
            GoogleSheetOptions googleSheetOptions = new GoogleSheetOptions
            {
                SheetId = "1oj3VIgzIFTV7vr3X8yQhgFj88ygU0oK_v8gnaJM7elw",
                SheetRange = $"Dictionary!A{startRowId}:B{Step}",
            };

            var value = await googleSheetsManager.Read(googleSheetOptions);

            List<Card> cards = new List<Card>();

            await Task.Run(async () =>
            {
                Random randomColor = new Random();

                foreach (var item in value)
                {
                    //Delay to random class
                    await Task.Delay(1);

                    string colorHex = ColorsList[randomColor.Next(0, ColorsList.Count)] ?? "#FF0000";
                    Color cardColor = Color.FromHex(colorHex);

                    cards.Add(new Card
                    {
                        EnglishText = item[0].ToString(),
                        RussianText = item[1].ToString(),
                        DisplayOptions = new CartDisplayOptions
                        {
                            BackColor = cardColor
                        }
                    });
                }
            });

            Step += SelectionStep;
            return cards;
        }
    }
}
