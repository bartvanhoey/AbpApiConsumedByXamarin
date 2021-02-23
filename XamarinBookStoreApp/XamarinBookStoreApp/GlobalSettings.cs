namespace XamarinBookStoreApp
{
    public class GlobalSettings
    {
        private readonly string _apiEndpoint = "https://192.168.1.106:44323/api/app/";
        public static GlobalSettings Instance { get; } = new GlobalSettings();
        public string PostBookUri => _apiEndpoint + "book";
        public string GetBooksUri => _apiEndpoint + "book";
        public string DeleteBookUri => _apiEndpoint + "book";
    }
}
