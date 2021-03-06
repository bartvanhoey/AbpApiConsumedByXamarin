﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBookStoreApp.Services;
using XamarinBookStoreApp.Services.Books;
using XamarinBookStoreApp.Services.Books.Dtos;
using XamarinBookStoreApp.Services.Http;
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
            DependencyService.Register<IdentityService>();
            DependencyService.Register<BooksService>();
            DependencyService.Register<HttpClientService<BookDto, CreateBookDto>>();

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
