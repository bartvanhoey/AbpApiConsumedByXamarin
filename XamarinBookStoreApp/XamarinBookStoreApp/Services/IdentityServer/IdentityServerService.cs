using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace XamarinBookStoreApp.Services.IdentityServer
{
    public class IdentityServerService : IIdentityServerService
    {
        public OidcClient GetOidcClient()
        {
            var browser = DependencyService.Get<IBrowser>();
            var options = new OidcClientOptions
            {
                Authority = "https://192.168.1.106:44368",
                ClientId = "XamarinBookStoreApi_Xamarin",
                Scope = "email openid profile role phone address XamarinBookStoreApi",
                ClientSecret = "1q2w3e*",
                RedirectUri = "xamarinformsclients://callback",
                Browser = browser,
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect
            };
            options.BackchannelHandler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true };
            options.Policy.Discovery.RequireHttps = true;
            return new OidcClient(options);
        }
    }
}
