using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBookStoreApp.Services;
using XamarinBookStoreApp.Services.Books;
using XamarinBookStoreApp.Services.IdentityServer;
using XamarinBookStoreApp.Views;

namespace XamarinBookStoreApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<BooksDataStore>();
            DependencyService.Register<IdentityServerService>();
            DependencyService.Register<BooksService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
