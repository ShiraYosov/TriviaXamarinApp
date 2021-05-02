using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;


namespace TriviaXamarinApp.ViewModels
{
    class RegisterViewModel: INotifyPropertyChanged
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
        }

        ICommand RegisterCommand => new Command(Register);

        public async void Register()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = new User
            {
                NickName = nickName,
                Password = password,
                Email = email
            };

            bool ok = await proxy.RegisterUser(u);
            if (ok) { Console.WriteLine("Registered successfully"); }
            else { Console.WriteLine("Failed to register"); }
        }
    }
}
