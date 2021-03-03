using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBookStoreApp.Services.Books.Dtos;
using XamarinBookStoreApp.Services.IdentityServer;

namespace XamarinBookStoreApp.Services.Http
{
    public class HttpClientService<T, C> : IHttpClientService<T, C> where T : class where C : class
    {
        public IIdentityService IdentityService => DependencyService.Get<IIdentityService>();
        Lazy<HttpClient> _httpClient;
        private async Task<string> GetAccessTokenAsync() => await IdentityService.GetAccessTokenAsync();


        public async Task<ListResult<T>> GetAsync(string uri)
        {
            _httpClient = await GetHttpClientAsync();
            var response = await _httpClient.Value.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListResult<T>>(content);

            var bookDto = result.Items.FirstOrDefault() as BookDto;
            var id = bookDto.Id;
            

            return result;
        }

        public async Task<T> CreateAsync(string uri, C createInputDto)
        {
            _httpClient = await GetHttpClientAsync();
            var data = JsonConvert.SerializeObject(createInputDto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.Value.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(stringResult);
            return result;
        }

        private HttpClientHandler GetHttpClientHandler()
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // EXCEPTION : Javax.Net.Ssl.SSLHandshakeException: 'java.security.cert.CertPathValidatorException: Trust anchor for certification path not found.'
            // SOLUTION :

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            return httpClientHandler;
        }

        public async Task DeleteAsync(string uri, string id)
        {
            _httpClient = await GetHttpClientAsync();
            var response = await _httpClient.Value.DeleteAsync($"{uri}/{id}");
            response.EnsureSuccessStatusCode();
        }

        private async Task<Lazy<HttpClient>> GetHttpClientAsync() {
            var accessToken = await GetAccessTokenAsync();
            var httpClient = new Lazy<HttpClient>(() => new HttpClient(GetHttpClientHandler()));
            httpClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken ?? "");
            return httpClient;
        }
    }
}
