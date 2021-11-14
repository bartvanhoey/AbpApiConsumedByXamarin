using Android.App;
using Android.Content;
using Android.Content.PM;

namespace AbpMauiApp.Platforms.Android
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
	[IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataScheme = "mauiclients")]
	public class WebAuthenticationCallbackActivity : Microsoft.Maui.Essentials.WebAuthenticatorCallbackActivity
	{
	}
}
