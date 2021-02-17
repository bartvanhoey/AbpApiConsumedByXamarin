using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace XamarinBookStoreApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<XamarinBookStoreApiIdentityServerModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
