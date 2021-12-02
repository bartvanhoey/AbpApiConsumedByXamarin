using AbpXamarinForms.Services;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;

namespace AbpXamarinForms.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly LoginService _loginService = new LoginService();
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var accessToken = await _loginService.AuthenticateAsync();
            Console.WriteLine($"accesstoken: {accessToken}");

            var httpClient = GetHttpClient(accessToken);
            var response = await httpClient.Value.GetAsync("https://7bb4-2a02-810d-98c0-576c-a0b1-8ff0-f58c-8179.eu.ngrok.io/api/app/book");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var booksResult = JsonConvert.DeserializeObject<BooksResult>(content);

                var book = booksResult.Items.FirstOrDefault();
                Console.WriteLine($"book: {book.Name} - price: {book.Price}");
            }
            // Set a breakpoint on the line below
            Console.ReadLine();
        }

        private Lazy<HttpClient> GetHttpClient(string accessToken)
        {
            var httpClient = new Lazy<HttpClient>(() => new HttpClient(GetHttpClientHandler()));
            httpClient.Value.SetBearerToken(accessToken);
            return httpClient;
        }

        private HttpClientHandler GetHttpClientHandler()
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // EXCEPTION : Javax.Net.Ssl.SSLHandshakeException: 'java.security.cert.CertPathValidatorException: Trust anchor for certification path not found.'
            // SOLUTION :
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            return httpClientHandler;
        }

    }

    public class BooksResult
    {
        public int TotalCount { get; set; }
        public List<BookDto> Items { get; set; }
    }

    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
    }

    public enum BookType
    {
        Undefined,
        Adventure,
        Biography,
        Dystopia,
        Fantastic,
        Horror,
        Science,
        ScienceFiction,
        Poetry
    }
}

