using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using SamReader.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SamReader
{
  // Learn more about making custom code visible in the Xamarin.Forms previewer
  // by visiting https://aka.ms/xamarinforms-previewer
  [DesignTimeVisible(false)]
  public partial class BookShelf : ContentPage
  {
    public BookShelf()
    {
      InitializeComponent();
      localAcc = App.LocalAccessor;

      ListBooks.ItemsSource = Books;
    }

    private readonly LocalAccessor localAcc;
    private readonly ObservableCollection<Book> Books = new ObservableCollection<Book>();
    private void OnDeleteClicked(object sender, EventArgs e)
    {
      Trace.TraceInformation("OnDeleteClicked");
    }
    private async void OnAddClicked(object sender, EventArgs e)
    {
      try
      {
        FileData fileData = await CrossFilePicker.Current.PickFile();
        if (fileData != null)
        {
          var book = new Book
          {
            LastAccessTime = DateTime.Now,
            Name = fileData.FileName,
            Path = fileData.FilePath,
          };
          await localAcc.SaveBookAsync(book);
          //Books.Insert(0, book);
        }
      }
      catch (Exception ex)
      {
        await DisplayAlert("Error", ex.Message, "OK");
      }
    }

    private async void TryLoadBooks()
    {
      try
      {
        Books.Clear();
        var books = await localAcc.GetBooksAsync();
        books.OrderByDescending(dd => dd.LastAccessTime)
         .ToList()
         .ForEach(dd => Books.Add(dd));
      }
      catch (Exception ex)
      {
        await DisplayAlert("Error", ex.Message, "OK");
      }
    }

    private async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
      if (e.Item != null)
      {
        var screen = new Read
        {
          BindingContext = e.Item
        };
        await Navigation.PushAsync(screen);
      }
    }

    protected override void OnAppearing()
    {
      TryLoadBooks();
      base.OnAppearing();
    }

   
  }
}