using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBookStoreApp.DomainShared.Books;

namespace XamarinBookStoreApp.ViewModels
{
    [QueryProperty(nameof(BookId), nameof(BookId))]
    public class BookDetailViewModel : BaseViewModel
    {
        private string bookId;
        private string name;
        private DateTime publishDate;
        private BookType type;
        private float price;
        public Command DeleteCommand { get; }
        public Command CancelCommand { get; }

        public BookDetailViewModel()
        {
            DeleteCommand = new Command(async () => await OnDelete());
            CancelCommand = new Command(OnCancel);
        }

        public float Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public BookType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        public DateTime PublishDate
        {
            get => publishDate;
            set => SetProperty(ref publishDate, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string BookId
        {
            get => bookId;
            set
            {
                bookId = value;
                LoadBook(value);
            }
        }
        public async void LoadBook(string bookId)
        {
            try
            {
                var bookDto = await BooksDataStore.GetBookAsync(Guid.Parse(bookId));
                Name = bookDto.Name;
                PublishDate = bookDto.PublishDate;
                Type = bookDto.Type;
                Price = bookDto.Price;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async Task OnDelete()
        {
            await BooksService.DeleteAsync(Guid.Parse(bookId));
            await Shell.Current.GoToAsync("..");
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

    }



}
