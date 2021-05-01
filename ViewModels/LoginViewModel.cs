using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using System.Text.Json;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;


namespace TriviaXamarinApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private string email;
        public string Email
        {
            get { return this.email; }

            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private string password;
        public string Password
        {
            get { return this.password; }

            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public LoginViewModel()
        {
            this.email = "";
            this.Password = "";
        }

        ICommand LoginCommand => new Command(Login);

        public async void Login()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(this.email, this.password);
            if (u == null) { Console.WriteLine("INVALID USER INFO"); }
            else
            {
                Page p = new QuestionsView();
                App.User.Email = this.email;
                App.User.Password = this.password;

                await App.Current.MainPage.Navigation.PushAsync(p);

            }

        }
    }
}
