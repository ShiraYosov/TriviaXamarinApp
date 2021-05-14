using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;


namespace TriviaXamarinApp.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string nickName;
        public string NickName
        {
            get { return this.nickName; }

            set
            {
                if (this.nickName != value)
                {
                    this.nickName = value;
                    OnPropertyChanged(nameof(NickName));
                }
            }
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

        public RegisterViewModel()
        {
            this.NickName = "";
            this.Password = "";
            this.Email = "";
            this.Message = "";
        }

        public ICommand RegisterCommand => new Command(Register);

        public async void Register()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            if (NickName != "" || Password!= "" || Email!="" )
            {
                User u = new User
                {
                    NickName = nickName,
                    Password = password,
                    Email = email,
                    Questions = null
                };

                bool ok = await proxy.RegisterUser(u);
                if (ok)
                {
                    Page p = new UsersPageView();
                    App a = (App)App.Current;
                    a.User.Email = this.email;
                    a.User.Password = this.password;
                    a.User.NickName = this.nickName;

                    await a.MainPage.Navigation.PushAsync(p);
                }

                else { this.Message = "Failed to register"; }
            }
            else
            { this.Message = "Failed to register! One field or more is missing"; }
        }
    }
}
