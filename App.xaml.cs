using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System.Threading.Tasks;
using TriviaXamarinApp.Views;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;


namespace TriviaXamarinApp
{
    public partial class App : Application, INotifyPropertyChanged
    {
        public User User;
        public App()
        {
            User = new User();
            User = null;
            InitializeComponent();
            Page p = new HomePageView();
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
