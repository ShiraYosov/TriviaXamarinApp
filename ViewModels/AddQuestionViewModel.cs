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
    class AddQuestionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string qText;
        public string QText
        {
            get { return this.qText; }

            set
            {
                if (this.qText != value)
                {
                    this.qText = value;
                    OnPropertyChanged(nameof(QText));
                }
            }
        }

        private string correctAnswer;
        public string CorrectAnswer
        {
            get { return this.correctAnswer; }

            set
            {
                if (this.correctAnswer != value)
                {
                    this.correctAnswer = value;
                    OnPropertyChanged(nameof(CorrectAnswer));
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


        private string ans1;
        public string Ans1
        {
            get { return this.ans1; }

            set
            {
                if (this.ans1 != value)
                {
                    this.ans1 = value;
                    OnPropertyChanged(nameof(Ans1));
                }
            }
        }
        private string ans2;
        public string Ans2
        {
            get { return this.ans2; }

            set
            {
                if (this.ans2 != value)
                {
                    this.ans2 = value;
                    OnPropertyChanged(nameof(Ans2));
                }
            }
        }

        private string ans3;
        public string Ans3
        {
            get { return this.ans3; }

            set
            {
                if (this.ans3 != value)
                {
                    this.ans3 = value;
                    OnPropertyChanged(nameof(Ans3));
                }
            }
        }
        public AddQuestionViewModel()
        {
            this.CorrectAnswer = "";
            this.Ans1 = "";
            this.Ans2 = "";
            this.Ans3 = "";
            this.QText = "";
            this.Message = "";
        }

        public ICommand AddCommand => new Command(Add);
        public async void Add()
        {

            string[] arr = new string[3];
            arr[0] = Ans1;
            arr[1] = Ans2;
            arr[2] = Ans3;

            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            App a = (App)App.Current;
            AmericanQuestion aq = new AmericanQuestion
            {
                CorrectAnswer = correctAnswer,
                QText = qText,
                OtherAnswers = arr,
                CreatorNickName = a.User.NickName
            };
            bool ok = await proxy.PostNewQuestion(aq);

            if (ok)
            {
                Message = "Question was added successfully";
            }
            else { Message = "Couls not add your question"; }

        }
    }
}
