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



        private string message;
        public string Message
        {
            get { return this.message; }

            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
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
            this.Email = "";
            this.Password = "";
            this.Message = "";
        }

        public ICommand LoginCommand => new Command(Login);

        public async void Login()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(Email, Password);

            if (u != null)
            {
                App a = (App)App.Current;
                a.User = new User
                {
                    NickName = u.NickName,
                    Password = u.Password,
                    Email = u.Email,
                    Questions = u.Questions
                };
                Page p = new UsersPageView();
                
                await a.MainPage.Navigation.PushAsync(p);

            }
            else
            {
                this.Message = "INVALID USER INFO!";
            }

        }
    }
}
