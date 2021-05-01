using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System.Threading.Tasks;
using TriviaXamarinApp.Views;


namespace TriviaXamarinApp
{
    public partial class App : Application
    {
        static public User User;
        public App()
        {
            InitializeComponent();
            User = new User();
            Page p = new HomePageView();
            MainPage = p;
            MainPage = new NavigationPage(p);

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
