using System.Threading.Tasks;

namespace AbpApi.Data
{
    public interface IAbpApiDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
