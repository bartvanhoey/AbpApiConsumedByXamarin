using System.Threading.Tasks;
using XamarinBookStoreApp.Services.Books;

namespace XamarinBookStoreApp.Services.Http
{
    public interface IHttpClientService<T, C> where T : class where C : class
    {
        Task<GetResult<T>> GetAsync(string uri);
        Task<T> CreateAsync(string uri, C createInputDto);
        Task DeleteAsync(string uri, string id);
    }


}
