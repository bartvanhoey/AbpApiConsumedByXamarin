using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Models.Books;
using XamarinBookStoreApp.Services.Books;
using XamarinBookStoreApp.Services.Books.Dtos;

namespace XamarinBookStoreApp.ViewModels
{
    public class AddBookViewModel : BaseViewModel
    {
        public IBooksDataStore<BookDto> BooksDataStore => DependencyService.Get<IBooksDataStore<BookDto>>();
        public IBooksService BooksService => DependencyService.Get<IBooksService>();


        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private DateTime _publishDate;
        public DateTime PublishDate
        {
            get { return _publishDate; }
            set { SetProperty(ref _publishDate, value); }
        }

        private BookType _type;
        public BookType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        private float _price;
        public float Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        public DateTime MaximuPublisHdate { get; set; } = DateTime.Now;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public AddBookViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_name)
                && _price > 0 && _publishDate != null;
        }


        private async void OnSave()
        {
            var newBook = new BookDto()
            {
                Id = Guid.NewGuid(),

            };

            await BooksService.CreateAsync(new CreateBookDto { Name = Name, Price = Price, PublishDate = PublishDate, Type = Type });
            await BooksDataStore.AddBookAsync(newBook);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
