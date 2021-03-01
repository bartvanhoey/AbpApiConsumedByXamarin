using System.Collections.Generic;

namespace XamarinBookStoreApp
{
    public class Global
    {
        public IdentityServer IdentityServer { get; }
        public CustomClaims CustomClaims { get; }
        public Api Api { get; }

        public Global(IdentityServer identityServer, CustomClaims customClaims, Api api)
        {
            IdentityServer = identityServer;
            CustomClaims = customClaims;
            Api = api;
        }

        public static Global Settings { get; } = new Global(new IdentityServer(), new CustomClaims(), new Api());
    }

    public class IdentityServer
    {
        public readonly string Authority = "https://192.168.1.104:44368";
        public readonly string ClientId = "XamarinBookStoreApi_Xamarin";
        public readonly string Scope = "email openid profile role phone address XamarinBookStoreApi";
        public readonly string ClientSecret = "1q2w3e*";
        public readonly string RedirectUri = "xamarinformsclients://callback";
    }

    public class CustomClaims
    {
        public readonly IEnumerable<string> Get = new List<string> { Permissions.Books.Default, Permissions.Books.Create, Permissions.Books.Edit, Permissions.Books.Delete };
    }

    public class Api
    {
        private readonly string _apiEndpoint = "https://192.168.1.104:44323/api/app/";
        public string PostBookUri => _apiEndpoint + "book";
        public string GetBooksUri => _apiEndpoint + "book";
        public string DeleteBookUri => _apiEndpoint + "book";
    }

    public static class Permissions
    {
        public const string GroupName = "BookStore";

        public class Books
        {
            public const string Default = GroupName + ".Books";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}
