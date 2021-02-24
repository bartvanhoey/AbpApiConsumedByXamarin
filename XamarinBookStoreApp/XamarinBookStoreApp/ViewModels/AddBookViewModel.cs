using System;
using Xamarin.Forms;
using XamarinBookStoreApp.DomainShared.Books;
using XamarinBookStoreApp.Services.Books.Dtos;

namespace XamarinBookStoreApp.ViewModels
{
    public class AddBookViewModel : BaseViewModel
    {
        private string _name;
        private DateTime _publishDate;
        private BookType _type;
        private float _price;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public AddBookViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public DateTime PublishDate
        {
            get { return _publishDate; }
            set { SetProperty(ref _publishDate, value); }
        }

        public BookType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        public float Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        public DateTime MaximumPublishDate { get; set; } = DateTime.Now;

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
