using System.Collections.Generic;

namespace XamarinBookStoreApp
{
    public class Global
    {
        public IdentityServer IdentityServer { get; }
        public PossiblePermissions PossiblePermissions { get; }
        public Api Api { get; }

        public Global(IdentityServer identityServer, PossiblePermissions possiblePermissions, Api api)
        {
            IdentityServer = identityServer;
            PossiblePermissions = possiblePermissions;
            Api = api;
        }

        public static Global Settings { get; } = new Global(new IdentityServer(), new PossiblePermissions(), new Api());
    }

    public class IdentityServer
    {
        public readonly string Authority = "https://192.168.1.108:44368";
        public readonly string ClientId = "XamarinBookStoreApi_Xamarin";
        public readonly string Scope = "email openid profile role phone address XamarinBookStoreApi";
        public readonly string ClientSecret = "1q2w3e*";
        public readonly string RedirectUri = "xamarinformsclients://callback";
    }

    public class PossiblePermissions
    {
        public readonly IEnumerable<string> Get = Permissions.GetPossiblePermissions();
    }

    public class Api
    {
        private readonly string _apiEndpoint = "https://192.168.1.108:44323/api/app/";
        public string PostBookUri => _apiEndpoint + "book";
        public string GetBooksUri => _apiEndpoint + "book";
        public string DeleteBookUri => _apiEndpoint + "book";
    }
}
