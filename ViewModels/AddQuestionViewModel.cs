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
        public ObservableCollection<string> AnswersList { get; set; }

        public AddQuestionViewModel()
        {
            this.CorrectAnswer = "";
            this.AnswersList = new ObservableCollection<string>();
            this.QText = "";
            this.Message = "";
        }

        public ICommand AddCommand => new Command(Add);
        public async void Add()
        {
            int x = 0;
            string[] arr = new string[3];
            foreach(string s in AnswersList)
            {
                arr[x] = s;
                x++;
            }
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

            if(ok)
            {
                Message = "Question was added successfully";
            }
            else { Message = "Couls not add your question"; }
            
        }
    }
}
