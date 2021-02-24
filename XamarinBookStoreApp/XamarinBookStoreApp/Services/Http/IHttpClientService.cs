using System.Threading.Tasks;
using XamarinBookStoreApp.Services.Books.Dtos;

namespace XamarinBookStoreApp.Services.Http
{
    public interface IHttpClientService<T, C> where T : class where C : class
    {
        Task<ListResult<T>> GetAsync(string uri);
        Task<T> CreateAsync(string uri, C createInputDto);
        Task DeleteAsync(string uri, string id);
    }


}
