using English_Flashcards.Models;
using Google.Apis.Sheets.v4;
using GoogleAPI_Library;
using GoogleAPI_Library.Models;

namespace English_Flashcards.Services
{
    class GoogleSheetService
    {
        private readonly GoogleSheetsManager googleSheetsManager;

        #region Colors
        private static List<string> ColorsList = new List<string>
        {
            "#FFCCCC",
            "#EEC3BD",
            "#AEA4A2",
            "#C07A65",
            "#FF8962",
            "#B78D6F",
            "#E2E2CF",
            "#BCC37A",
            "#E9FBA6",
            "#64C2C2",
            "#0EBFE9",
        };
        #endregion

        public GoogleSheetService(Stream ClientSecretStream)
        {

            var data = FileSystem.Current.AppDataDirectory + "\\Resources\\GoogleSheetsSecret.json";

            GoogleCredentialOptions_Stream googleCredential = new GoogleCredentialOptions_Stream
            {
                ApplicationName = "Dictionary-Translator",
                Scopes = new string[] { SheetsService.Scope.Spreadsheets },
                ClientSecretStream = ClientSecretStream
            };

            googleSheetsManager = new GoogleSheetsManager(googleCredential);
        }

        internal async Task<IEnumerable<Card>> GetCards()
        {
            GoogleSheetOptions googleSheetOptions = new GoogleSheetOptions
            {
                SheetId = "1oj3VIgzIFTV7vr3X8yQhgFj88ygU0oK_v8gnaJM7elw",
                SheetRange = "Dictionary!A2:B",
            };

            var value = await googleSheetsManager.Read(googleSheetOptions);

            List<Card> cards = new List<Card>();
            Random randomColor = new Random();

            foreach (var item in value)
            {
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

            return cards;
        }
    }
}
