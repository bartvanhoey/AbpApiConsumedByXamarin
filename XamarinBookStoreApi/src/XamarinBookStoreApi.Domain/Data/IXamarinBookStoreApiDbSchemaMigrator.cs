using System.Threading.Tasks;

namespace XamarinBookStoreApi.Data
{
    public interface IXamarinBookStoreApiDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
