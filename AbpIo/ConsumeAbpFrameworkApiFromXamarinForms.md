## ABP Framework consumed by a Xamarin.Forms application

## Introduction

In this article I will explain **how to consume an ABP Framework API with Xamarin.Forms**.

The article is a complete rewrite of an older article that you can find here [here](https://github.com/bartvanhoey/AbpApiConsumedByXamarin/blob/main/AbpIo/ConsumeAbpFrameworkApiFromXamarinForms_old.md).

## Source Code

The sample application has been developed with Blazor as UI framework and SQL Server as database provider.

The Source code of the completed application is [available on GitHub](https://github.com/bartvanhoey/AbpApiConsumedByXamarin/tree/main/XamarinForms).

## Requirements

The following tools are needed to be able to run the solution and follow along.
You will also need to have your editor set up for Xamarin.Forms development.

* .NET 5.0 SDK
* VsCode, Visual Studio 2019, or another compatible IDE.

## Create a new ABP Framework application

```bash
    abp new AbpApi -u blazor -o AbpApi
```

### BookAppService (optional)

To have a simple API that you can consume with the Xamarin.Forms app, add the Books Bookstore code from the BookStore Tutorial (Part1-5).

### Add AbpApi_Xamarin section in appsettings.json file of the AbpApi.DbMigrator project

```json
    // change the <replace-me-with-the-abp-api-port> with the port were the Swagger page is running on
    "AbpApi_Xamarin": {
        "ClientId": "AbpApi_Xamarin",
        "ClientSecret": "1q2w3e*",
        "RootUrl": "https://localhost:<replace-me-with-the-abp-api-port>/" 
    }
```

### Add Xamarin client IdentityServer configuration

In the CreateClientAsync method in class IdentityServerDataSeedContributor of the AbpApi.Domain project.

```csharp
    // Xamarin Client
    var xamarinClientId = configurationSection["AbpApi_Xamarin:ClientId"];
    if (!xamarinClientId.IsNullOrWhiteSpace())
    {
        var xamarinRootUrl = configurationSection["AbpApi_Xamarin:RootUrl"].TrimEnd('/');
        await CreateClientAsync(
            name: xamarinClientId,
            scopes: commonScopes,
            grantTypes: new[] { "authorization_code" },
            secret: configurationSection["AbpApi_Xamarin:ClientSecret"]?.Sha256(),
            requireClientSecret: false,
            redirectUri: "xamarinformsclients:/authenticated",
            postLogoutRedirectUri: "xamarinformsclients:/signout-callback-oidc",
            corsOrigins: new[] { xamarinRootUrl.RemovePostFix("/") }
        );
    }
```

### Insert XamarinClient setting into Database

Run AbpApi.DbMigrator project to execute the IdentityServerDataSeedContributor to insert the XamarinClient settings into the database.

### Start API and Blazor project

Start API and Blazor project to see if all projects are running successfully. Keep the API running!

## Download & setup ngrok

With ngrok you can mirror your localhost API endpoint to a worldwide available API endpoint.
In this way you can overcome the problem Xamarin.Forms app mixing up localhost from the API with localhost from the Xamarin.Forms app.

### Open a command prompt in the root of ABP Framework application and run the command below

```bash
    -- specify another region when needed
    ngrok http -region eu https://localhost:<replace-me-with-the-abp-api-port>/ 
```

![Ngrok port forwarding](https://github.com/bartvanhoey/AbpApiConsumedByXamarin/blob/main/Images/ngrok_localhost_port_forwarding.jpg)

### Copy and remember Ngrok Forwarding https endpoint

```bash
    "https://<your-ngrok-generated-generated-number-here>.eu.ngrok.io"
```

## Create a new Xamarin.Forms application

### Create a new Xamarin app in Visual Studio (Flyout template)

![Create a new Xamarin.Forms app](Images/create_new_mobile_app.jpg)

### Update Nuget Packages

I updated the following nuget packages in the Xamarin.core project and the Android.project.

```bash
    Xamarin.Forms" Version="5.0.0.2244
    Xamarin.Essentials" Version="1.7.0
```

### Add a FlyoutItem in file AppShell.xaml of the AbpXamarinForms core project

```html
    <FlyoutItem Title="Login" Icon="icon_about.png">
        <ShellContent Route="LoginPage" ContentTemplate="{DataTemplate local:LoginPage}" />
    </FlyoutItem>
    // ... other FlyoutItems here
```

### Run XamarinForms application

Start the Android the Xamarin.Forms application and stop it again when it runs successfully.

## Connect to AbpApi IdentityServer

### Install IdentityModel and IdentityModel.OidcClient nuget packages

Open the Nuget Package Manager and install **IdentityModel**, **IdentityModel.OidcClient**  and **Newtonsoft.json** nuget packages in the core project.

![Installed nuget packages](Images/installed_nuget_packages_in_xamarin_forms.jpg)

### Add a WebAuthenticatorBrowser class to the Services folder in the Core project

This class is needed to open a browser page in your Xamarin.Forms application.

```csharp
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.OidcClient.Browser;
using Xamarin.Essentials;

namespace AbpXamarinForms.Services
{
    internal class WebAuthenticatorBrowser : IBrowser
    {
        private readonly string _callbackUrl;

        public WebAuthenticatorBrowser(string callbackUrl = null) => _callbackUrl = callbackUrl ?? "";

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            try
            {
                var callbackUrl = string.IsNullOrEmpty(_callbackUrl) ? options.EndUrl : _callbackUrl;
                var authResult =
                    await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(callbackUrl));
                var authorizeResponse = ToRawIdentityUrl(options.EndUrl, authResult);
                return new BrowserResult
                {
                    Response = authorizeResponse
                };
            }
            catch (Exception exception)
            {
                
                return new BrowserResult
                {
                    ResultType = BrowserResultType.UnknownError,
                    Error = exception.ToString()
                };
            }
        }

        private static string ToRawIdentityUrl(string redirectUrl, WebAuthenticatorResult result)
        {
            var parameters = result.Properties.Select(pair => $"{pair.Key}={pair.Value}");
            var values = string.Join("&", parameters);
            return $"{redirectUrl}#{values}";
        }
    }
}
```

### Add a LoginService class to the Services folder

```csharp
using IdentityModel.OidcClient;
using System.Threading.Tasks;

namespace AbpXamarinForms.Services
{
    public class LoginService
    {
        private const string _authorityUrl = "https://<your-ngrok-generated-generated-number-here>.eu.ngrok.io";
        private const string _redirectUrl = "xamarinformsclients:/authenticated";
        private const string _postLogoutRedirectUrl = "xamarinformsclients:/signout-callback-oidc";
        private const string _scopes = "email openid profile role phone address AbpApi";
        private const string _clientSecret = "1q2w3e*";
        private const string _clientId = "AbpApi_Xamarin";


        private OidcClient CreateOidcClient()
        {
            var options = new OidcClientOptions
            {
                Authority = _authorityUrl,
                ClientId = _clientId,
                Scope = _scopes,
                RedirectUri = _redirectUrl,
                ClientSecret = _clientSecret,
                PostLogoutRedirectUri = _postLogoutRedirectUrl,
                Browser = new WebAuthenticatorBrowser()
            };
            return new OidcClient(options);
        }

         public async Task<string> AuthenticateAsync()
        {
            var oidcClient = CreateOidcClient();
            var loginResult = await oidcClient.LoginAsync(new LoginRequest());
            return loginResult.AccessToken;
        }
    }
}

```

### Update content of the LoginViewModel.cs class

```csharp
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
            var ngRokUrl = "https://<your-ngrok-generated-generated-number-here>.eu.ngrok.io";
            var accessToken = await _loginService.AuthenticateAsync();
            Console.WriteLine($"accesstoken: {accessToken}");

            var httpClient = GetHttpClient(accessToken);
            var response = await httpClient.Value.GetAsync($"{ngRokUrl}/api/app/book");
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
```

### Add a WebAuthenticationCallbackActivity class in root of the Android project

```csharp
using Android.App;
using Android.Content;
using Android.Content.PM;

namespace AbpXamarinForms.Droid
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataScheme = "xamarinformsclients")]
    public class WebAuthenticationCallbackActivity : Xamarin.Essentials.WebAuthenticatorCallbackActivity
    {
    }
}
```

## Run both API and Xamarin.Forms application

* Update the **_authorityUrl** field in the LoginService class with the correct **ngrok Forwarding https url**
* Update the **ngRokUrl** variable in the **OnLoginClicked** method of the **LoginViewModel** class
* Start the **AbpApi** application and make sure **ngrok is running**
* Run the **AbpXamarinForms** application on an emulator or physical device.
* Click the **Login button** and enter the **administrator credentials** (admin, 1q2w3E*)

**WARNING**: You will probably get a **SecurityTokenInvalidIssuerException**

## Fix SecurityTokenInvalidIssuerException: IDX10205: Issuer validation failed

```bash
Failed to validate the token.

Microsoft.IdentityModel.Tokens.SecurityTokenInvalidIssuerException: IDX10205: Issuer validation failed. Issuer: 'System.String'. Did not match: validationParameters.ValidIssuer: 'System.String' or validationParameters.ValidIssuers: 'System.String'.
   at Microsoft.IdentityModel.Tokens.Validators.ValidateIssuer(String issuer, SecurityToken securityToken, TokenValidationParameters validationParameters)
   at System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.ValidateIssuer(String issuer, JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
   at System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.ValidateTokenPayload(JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
```

### Update the ConfigureAuthentication method in the AbpApiHostModule of the AbpApi.HttpApi.Host project

```csharp
 private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
{
    context.Services.AddAuthentication()
        .AddJwtBearer(options =>
        {
            options.Authority = configuration["AuthServer:Authority"];
            options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
            options.Audience = "AbpApi";
            options.BackchannelHttpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            // Add the line below 
            options.TokenValidationParameters.ValidIssuers = configuration.GetSection("AuthServer:ValidIssuers").Get<string[]>();
            
            // Alternatively the line below would also fix the problem.
            // options.TokenValidationParameters.ValidateIssuer = false;
        });
}
```

### Add ValidIssuers to the AuthServer section of the appsettings.json file in the AbpApi.HttpApi.Host project

```json
"AuthServer": {
    "Authority": "https://localhost:<replace-me-with-the-abp-api-port>",
    "RequireHttpsMetadata": "false",
    "SwaggerClientId": "AbpApi_Swagger",
    "SwaggerClientSecret": "1q2w3e*",
    "ValidIssuers": [
      "https://<your-ngrok-generated-generated-number-here>.eu.ngrok.io"
    ]
  },
```

## Start both the AbpApi and the AbpXamarinForms applications

If all goes well, your XamarinForms application opens the ABP login page where you can enter the administrator credentials (admin - 1q2w3E*).
Once logged in, the app makes a call to the ABP Framework API to get the books from the database.

Et voilà! The Xamarin.Forms app connects to the IdentityServer4 successfully and gets the books from the ABP Framework API.

Get the [source code](https://github.com/bartvanhoey/AbpApiConsumedByXamarin/tree/main/XamarinForms) on GitHub.

Enjoy and have fun!
