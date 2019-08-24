using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamReader
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      MainPage = new NavigationPage(new BookShelf());
    }

    const string DB_FileName = "data.db3";
    private static Lazy<LocalAccessor> localAccessorLazy = new Lazy<LocalAccessor>(() => new LocalAccessor(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DB_FileName)));
    public static LocalAccessor LocalAccessor => localAccessorLazy.Value;

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
