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
        public ObservableCollection<string> AnswersList { get; set; }

        public AddQuestionViewModel()
        {
            this.CorrectAnswer = "";
            this.AnswersList = new ObservableCollection<string>();
            this.QText = "";
        }

        public ICommand AddCommand => new Command(Add);
        public async void Add()
        {
            string[] arr = new string[3];
            foreach(string s in AnswersList)
            {
                
            }
            AmericanQuestion a = new AmericanQuestion
            {
                CorrectAnswer = correctAnswer,
                QText = qText,
                OtherAnswers= AnswersList
            };
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            
        }
    }
}
